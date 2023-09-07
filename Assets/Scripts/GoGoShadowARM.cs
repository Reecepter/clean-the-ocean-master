using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoGoShadowARM : MonoBehaviour {

    private Camera playerCamera;

    public GameObject trackedObj;

    public GameObject cameraRig; // So shadow can attach itself to the camera rig on game start

    // ARM Integration
    //private SteamVR_Controller.Device Controller
    //{
    //    get
    //    {
    //        return SteamVR_Controller.Input((int)trackedObj.index);
    //    }
    //}

    public float armLength; // Either manually inputted or will be set to the arm length when calculated

    public float distanceFromHeadToChest = 0.3f; // estimation of the distance from the users headset to their chest area

    public GameObject theController; // controller for the gogo to access inout

    public GameObject theModel; // the model of the controller that will be shadowed for gogo use

    public float extensionVariable = 120f; // this variable in the equation controls the multiplier for how far the arm can extend with small movements
    
    Vector3 chestPosition;
    Vector3 relativeChestPos;

    // ARM Variables
    private bool ARMOn = false;
    private bool prevARMOn = false;
    private Vector3 lastDirectionPointing;
    private Quaternion lastRotation;
    private Vector3 lastPosition;
    public GameObject shadowGoGo;

    private bool isCloseGoGo;
    // velocity tracking
    LinkedList<float> velocities = new LinkedList<float>();
    float velocity = 0.0f;
    Vector3 previousPosition;
    float velocityThreshold = 0.2f;

    // ARM Controller for Toggling
    public enum ControllerState
    {
        TRIGGER_DOWN, TOUCHPAD_UP, TOUCHPAD_DOWN, NONE,
        TRIGGER_HALF_DOWN
    }
    
    void makeModelChild() {
        if (this.transform.childCount == 0) {
            if (theModel != null) { // The steamVR_RenderModel is generated after code start so we cannot parent right away or it wont generate. 
                if (theModel.transform.childCount > 0) {
                    theModel.transform.parent = this.transform;
                    // Due to the transfer happening at a random time down the line we need to re-align the model inside the shadow controller to 0 so nothing is wonky.
                    theModel.transform.localPosition = Vector3.zero;
                    theModel.transform.localRotation = Quaternion.identity;
                }
            } else {
                // If it is just a custom model we can immediately parent
                theModel.transform.parent = this.transform;
                // Due to the transfer happening at a random time down the line we need to re-align the model inside the shadow controller to 0 so nothing is wonky.
                theModel.transform.localPosition = Vector3.zero;
                theModel.transform.localRotation = Quaternion.identity;
            }
        }

    }

    // Might have to have a manuel calibration for best use
    float getDistanceToExtend() {
        // estimating chest position using an assumed distance from head to chest and then going that distance down the down vector of the camera. This will not allways be optimal especially when leaning is involved.
        // To improve gogo to suite your needs all you need to do is implement your own algorithm to estimate chest (or shoulder for even high accuracy) position and set the chest position vector to match it

        Vector3 direction = playerCamera.transform.up * -1;
        Vector3 normalizedDirectionPlusDistance = direction.normalized * distanceFromHeadToChest;
        chestPosition = playerCamera.transform.position + normalizedDirectionPlusDistance;

        float distChestPos = Vector3.Distance(trackedObj.transform.position, chestPosition);

        float D = (2f * armLength) / 3f; // 2/3 of users arm length

        //D = 0;
        if (distChestPos >= D) {
            float extensionDistance = distChestPos + (extensionVariable * (float)Math.Pow(distChestPos - D, 2));
            // Dont need both here as we only want the distance to extend by not the full distance
            // but we want to keep the above formula matching the original papers formula so will then calculate just the distance to extend below
            return extensionDistance - distChestPos;
        }
        return 0; // dont extend
    }

    // Use this for initialization
    void Start() {
        this.transform.parent = cameraRig.transform;
        if (Camera.main != null) {
            playerCamera = Camera.main;
        } else {
            playerCamera = cameraRig.GetComponentInChildren<Camera>();
        }
        makeModelChild();

        lastDirectionPointing = theController.transform.forward;
        lastRotation = theController.transform.rotation;
        lastPosition = theController.transform.position;

        previousPosition = theController.transform.position;

    }
    
    // Update is called once per frame
    void Update()
    {

        makeModelChild();
        //this.GetComponentInChildren<SteamVR_RenderModel>().gameObject.SetActive(false);
        Renderer[] renderers = this.transform.parent.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.name == "Standard (Instance)")
            {
                renderer.enabled = true;
            }
        }

        // Calculate the GoGo position of the controller
        Vector3 gogoPos = moveControllerForward();

        /************************************************************
        * Toggle ARM/GoGo with Controller Speed START
        * **********************************************************/
        
        updatePositionAndRotationToFollowControllerSpeed(gogoPos, velocity);
        updateGripMaterial();

        // GoGo is active and the controller movement is slow for a while (for the last 20 frames)
        if (!ARMOn  && velocities.Count > 0 && velocities.First.Value < velocityThreshold && velocity < velocityThreshold)
        {
            lastDirectionPointing = trackedObj.transform.forward;
            lastRotation = this.transform.rotation;
            lastPosition = this.transform.position;
            Debug.Log("ARM ON Last Position: " + lastPosition);

            prevARMOn = ARMOn;
            ARMOn = !ARMOn;
        }
        else if (velocity >= velocityThreshold)
        {
            prevARMOn = ARMOn;
            ARMOn = false;
        }
        /************************************************************
         * Toggle ARM/GoGo with Controller Speed END
         * **********************************************************/

        if (Vector3.Distance(transform.position, theController.transform.position) <= 1f)
        {
            isCloseGoGo = true;
        }
        else
        {
            isCloseGoGo = false;
        }

    }

    private void FixedUpdate()
    {
        // get the velocity of the controller
        Vector3 currentPosition = theController.transform.position;
        float distanceTravelled = (currentPosition - previousPosition).magnitude;
        velocity = distanceTravelled / Time.deltaTime;
        previousPosition = currentPosition;

        Debug.Log("Controller Velocity: " + velocity);

        // don't store the velocity if the controller is completely static
        if (velocity > 0)
        {
            velocities.AddLast(velocity);
        }

        // keep a finite queue of the past velocities
        if (velocities.Count > 20)
        {
            velocities.RemoveFirst();
        }
    }

    void updatePositionAndRotationToFollowControllerSpeed(Vector3 gogoPosition, float controllerVelocity)
    {
        //Debug.Log("This Transform Position: " + this.transform.position);
        //Debug.Log("TrackedObject Transform Position: " + trackedObj.transform.position);
        //Debug.Log("TheController Transform Position: " + theController.transform.position);
        //Debug.Log("GoGo Position: " + gogoPosition);

        //this.transform.position = trackedObj.transform.position;
        Quaternion rotationOfDevice = trackedObj.transform.rotation;
        if (isCloseGoGo)
        {
            this.transform.rotation = trackedObj.transform.rotation;
            //StartCoroutine(moveToGoGo(gogoPosition));
            this.transform.position = gogoPosition;
            shadowGoGo.SetActive(false);
        }
        else if (ARMOn)
        {
            shadowGoGo.SetActive(true);
            shadowGoGo.transform.position = gogoPosition;
            shadowGoGo.transform.rotation = trackedObj.transform.rotation;

            // scaled down by factor of 10
            this.transform.rotation = Quaternion.Lerp(lastRotation, rotationOfDevice, 0.2f);
            this.transform.position = Vector3.Lerp(lastPosition, gogoPosition, 0.2f);

            print("ARM On");
        }
        else
        {
            shadowGoGo.SetActive(true);
            this.transform.rotation = trackedObj.transform.rotation;
            //StartCoroutine(moveToGoGo(gogoPosition));
            this.transform.position = gogoPosition;
            //this.transform.position = Vector3.MoveTowards(this.transform.position, gogoPosition, 1.0f * Time.deltaTime);
        }
    }

    //IEnumerator moveToGoGo(Vector3 gogoPosition)
    //{
    //    Tween myTween = this.transform.DOMove(gogoPosition, 2f);
    //    yield return myTween.WaitForCompletion();
    //}

    private void updateGripMaterial()
    {
        if(this.transform.GetComponent<SetMaterials>() == null)
        {
            return;
        }
        if(prevARMOn && !ARMOn)
        {
            this.transform.GetComponent<SetMaterials>().restoreMat();
        }
        if(!prevARMOn && ARMOn)
        {
            this.transform.GetComponent<SetMaterials>().changeMat();
        }
    }

    Vector3 moveControllerForward() {
        // Using the origin and the forward vector of the remote the extended positon of the remote can be calculated
        //Vector3 theVector = theController.transform.forward;
        Vector3 theVector = theController.transform.position - chestPosition;

        Vector3 pose = theController.transform.position;
        Quaternion rot = theController.transform.rotation;

        float distance_formula_on_vector = Mathf.Sqrt(theVector.x * theVector.x + theVector.y * theVector.y + theVector.z * theVector.z);

        float distanceToExtend = getDistanceToExtend();

        if (distanceToExtend != 0) {
            // Using formula to find a point which lies at distance on a 3D line from vector and direction
            pose.x = pose.x + (distanceToExtend / (distance_formula_on_vector)) * theVector.x;
            pose.y = pose.y + (distanceToExtend / (distance_formula_on_vector)) * theVector.y;
            pose.z = pose.z + (distanceToExtend / (distance_formula_on_vector)) * theVector.z;
        }
        
        return pose;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    public GameObject fishPrefab;
    public int numFish = 20;
    public GameObject[] allFish;
    public Vector3 swimLimits = new Vector3(10, 10, 10);
    public Vector3 flockSize = new Vector3(5, 5, 5);
    public int flockChangeFrequency = 10;
    [Header("(Can Choose 1-1000 for flock change frequency)")]
    [Header("")]
    public Vector3 goalPos;
    Vector3 center;
    //public  bool avoidObstacles;

    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighborDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;
	// Use this for initialization
	void Start () {

        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x), Random.Range(-swimLimits.y, swimLimits.y), Random.Range(-swimLimits.z, swimLimits.z));

            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
        }

        goalPos = this.transform.position;
        center = this.transform.position;

		
	}
	
	// Update is called once per frame
	void Update () {

        if(Random.Range(0,1000)< flockChangeFrequency)
        goalPos = this.transform.position + new Vector3(Random.Range(-flockSize.x, flockSize.x), Random.Range(-flockSize.y, flockSize.y), Random.Range(-flockSize.z, flockSize.z));
        center = this.transform.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(center, swimLimits);
        Gizmos.DrawCube(goalPos, flockSize);
    }
}

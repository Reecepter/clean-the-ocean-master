using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCoralChange : MonoBehaviour
{
    public CoralUnhealthy[] coralUnhealthies;
    
    public void ActivateUnhealthy()
    {
        for(int i = 0; i < coralUnhealthies.Length; i++)
        {
            coralUnhealthies[i].StartUnhealthy();
        }
    }
    public void ActivateHealthy()
    {
        for(int i = 0; i < coralUnhealthies.Length; i++)
        {
            coralUnhealthies[i].StartHealthy();
        }
    }
}

using System;
using UnityEngine;

public class WaterSpot : MonoBehaviour
{
    public ParticleSystem waterSpotParticleSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        waterSpotParticleSystem.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        waterSpotParticleSystem.Stop();
    }
}

using System;
using UnityEngine;

public class WaterSpot : MonoBehaviour
{
    public ParticleSystem waterSpotParticleSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        waterSpotParticleSystem.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        waterSpotParticleSystem.Stop();
    }
}

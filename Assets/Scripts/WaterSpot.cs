using UnityEngine;

public class WaterSpot : MonoBehaviour
{
    public ParticleSystem waterSpotParticleSystem;
    
    private void OnTriggerEnter(Collider other)
    {
        waterSpotParticleSystem.Play();
    }
}

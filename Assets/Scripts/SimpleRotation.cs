using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotation : MonoBehaviour
{
    public float speedX = 0.0f;
    public float speedY = 0.0f;
    public float speedZ = 0.3f;
    
    private void Update()
    {
        transform.Rotate(speedX, speedY, speedZ);
    }
}

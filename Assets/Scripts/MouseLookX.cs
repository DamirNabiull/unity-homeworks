using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookX : MonoBehaviour
{
    public float sensitivityHor = 3.0f;
    
    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if(body != null)
        {
            body.freezeRotation = true;
        }
    }
    
    private void Update()
    {
        float horizontalRot = Input.GetAxis("Mouse X") * sensitivityHor;
        transform.Rotate(0, horizontalRot, 0);
    }
}

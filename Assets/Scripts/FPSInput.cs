using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float defaultSpeed = 6.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;
    
    private void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var speed = GetSpeed();
        
        var deltaX = Input.GetAxis("Horizontal") * speed;
        var deltaZ = Input.GetAxis("Vertical") * speed;
        var dt = Time.deltaTime;
        
        var movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= dt;
        movement = transform.TransformDirection(movement);
        
        _charController.Move(movement);
    }

    private float GetSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return defaultSpeed * 2;
        }

        return defaultSpeed;
    }
}

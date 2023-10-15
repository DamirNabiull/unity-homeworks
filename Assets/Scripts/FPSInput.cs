using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float defaultSpeed = 6.0f;
    public float jumpSpeed = 2.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;
    private float _currentAcceleration;
    private bool _isCrouching;
    private static readonly Vector3 CrouchDelta = new Vector3(0f, 0.5f, 0f);
    
    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _currentAcceleration = gravity;
    }

    private void Update()
    {
        Move();
        Crouch();
    }

    private void Move()
    {
        var speed = GetSpeed();
        
        var deltaX = Input.GetAxis("Horizontal") * speed;
        var deltaZ = Input.GetAxis("Vertical") * speed;
        var dt = Time.deltaTime;
        
        var movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = _currentAcceleration;
        movement *= dt;

        if (_charController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            movement.y += jumpSpeed;
        }
        
        movement = transform.TransformDirection(movement);
        
        _charController.Move(movement);
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C) && _charController.isGrounded)
        {
            var playerTransform = transform;

            if (!_isCrouching)
            {
                playerTransform.localScale -= CrouchDelta;
                playerTransform.position -= CrouchDelta;
                _isCrouching = true;
            }
            else
            {
                playerTransform.localScale += CrouchDelta;
                playerTransform.position += CrouchDelta;
                _isCrouching = false;
            }
        }
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

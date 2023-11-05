using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip runSound;
    [SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip jumpSound;

    private static float _defaultSpeed = 6.0f;
    public float jumpSpeed = 2.0f;
    public float gravity = -9.8f;
    private CharacterController _charController;
    private float _currentAcceleration;
    private bool _isCrouching;
    private static readonly Vector3 CrouchDelta = new Vector3(0f, 0.5f, 0f);
    private int _sound = 0;
    private bool _isSoundPlaying = false;

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

        if (_isCrouching)
        {
            _isSoundPlaying = false;
            soundSource.Stop();
        } 
        else if (_charController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            movement.y += jumpSpeed;
            _isSoundPlaying = false;
            soundSource.PlayOneShot(jumpSound);
        }
        else if ((Mathf.Abs(movement.x) > 0.01 || Mathf.Abs(movement.z) > 0.01) && !_isSoundPlaying)
        {
            _isSoundPlaying = true;
            soundSource.Play();
        }
        else if (Mathf.Abs(movement.x) < 0.001 && Mathf.Abs(movement.z) < 0.001 && _charController.isGrounded)
        {
            _isSoundPlaying = false;
            soundSource.Stop();
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

                if (_sound != 0)
                {
                    _isSoundPlaying = true;
                    _sound = 0;
                    soundSource.Stop();
                }
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
            if (_sound != 1)
            {
                _isSoundPlaying = false;
                _sound = 1;
                soundSource.Stop();
                soundSource.clip = runSound;
            }

            return _defaultSpeed * 2;
        }

        if (_sound != 2)
        {
            _isSoundPlaying = false;
            _sound = 2;
            soundSource.Stop();
            soundSource.clip = walkSound;
        }
        
        return _defaultSpeed;
    }

    public static void SetSpeed(float newSpeed)
    {
        _defaultSpeed = newSpeed;
    }
}
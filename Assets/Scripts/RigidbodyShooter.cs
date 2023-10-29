using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyShooter : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    public float fireballSpeed = 20f;
    
    private Camera _cam;
    private const int CrosshairSize = 20;
    private static bool _inMenu = false;
    
    private void Start()
    {
        _cam = Camera.main;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            RightMouseClick();
        }
    }
    
    private void OnGUI()
    {
        float posX = _cam.pixelWidth / 2 - CrosshairSize / 4;
        float posY = _cam.pixelHeight / 2 - CrosshairSize / 2;
        GUI.Label(new Rect(posX, posY, CrosshairSize, CrosshairSize), "+");
    }
    
    private void RightMouseClick()
    {
        if (_inMenu) return;
        StartCoroutine(FireballShoot());
    }
    
    private IEnumerator FireballShoot()
    {
        var fireball = Instantiate(fireballPrefab, transform.position, transform.rotation);
        var fireballRigidbody = fireball.GetComponent<Rigidbody>();

        var xRotation = _cam.transform.rotation.x;
        var ySpeed = (float) -Math.Sin(xRotation) * fireballSpeed;
        var zSpeed = (float) Math.Cos(xRotation) * fireballSpeed;
        fireballRigidbody.velocity = transform.TransformDirection(new Vector3(0, ySpeed, zSpeed));
        yield return new WaitForSeconds(1);
        Destroy(fireball);
    }
    
    public static void SetInMenu(bool value)
    {
        _inMenu = value;
    }
}

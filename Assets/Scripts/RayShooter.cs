using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    [SerializeField] AudioSource soundSource;
    [SerializeField] AudioClip hitWallSound;
    [SerializeField] AudioClip hitEnemySound;
    
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
        if (Input.GetMouseButtonDown(0) && !_inMenu)
        {
            var ray = GetRay();
            if (Physics.Raycast(ray, out var hit))
            {
                if(!CheckIfReactiveTarget(hit))
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }
    
    private void OnGUI()
    {
        float posX = _cam.pixelWidth / 2 - CrosshairSize / 4;
        float posY = _cam.pixelHeight / 2 - CrosshairSize / 2;
        GUI.Label(new Rect(posX, posY, CrosshairSize, CrosshairSize), "+");
    }

    private Ray GetRay()
    {
        var point = new Vector3(_cam.pixelWidth / 2, _cam.pixelHeight / 2, 0);
        return _cam.ScreenPointToRay(point);
    }

    private bool CheckIfReactiveTarget(RaycastHit hit)
    {
        var hitObject = hit.transform.gameObject;
        var target = hitObject.GetComponent<ReactiveTarget>();
        if (target != null)
        {
            target.ReactToHit();
            soundSource.PlayOneShot(hitEnemySound);
            return true;
        }
        else
        {
            StartCoroutine(SphereIndicator(hit.point));
            soundSource.PlayOneShot(hitWallSound);
            return false;
        }
    }
    
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }

    public static void SetInMenu(bool value)
    {
        _inMenu = value;
    }
}

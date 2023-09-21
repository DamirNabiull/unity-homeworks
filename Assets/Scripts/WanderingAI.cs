using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    [SerializeField] GameObject fireballPrefab;
    
    private GameObject fireball;
    private bool _isAlive;
    private bool _isRotating = false;
    private const float RotationTime = 1f;
    private float _rotationLeftTime;
    private float _targetAngle;

    private void Start()
    {
        _isAlive = true;
    }
    
    private void Update()
    {
        if (_isAlive && !_isRotating)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        if (_isRotating)
        {
            transform.Rotate(new Vector3(0, (_targetAngle / RotationTime) * Time.deltaTime, 0));
            _rotationLeftTime -= Time.deltaTime;
            if (_rotationLeftTime <= 0f)
            {
                _isRotating = false;
            }
            else
            {
                return;
            }
        }
        
        var ray = new Ray(transform.position, transform.forward);
        if (Physics.SphereCast(ray, 0.75f, out var hit))
        {
            var hitObject = hit.transform.gameObject;
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab);
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }
            }
            else if(hit.distance < obstacleRange)
            {
                _isRotating = true;
                _targetAngle = Random.Range(-110, 110);
                _rotationLeftTime = RotationTime;
            }
        }
    }
    
    public void SetAlive(bool alive)
    {
        _isAlive = alive;
    }
}
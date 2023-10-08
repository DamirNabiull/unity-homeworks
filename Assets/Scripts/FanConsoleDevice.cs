using System;
using UnityEngine;

public class FanConsoleDevice : MonoBehaviour
{
    [SerializeField] GameObject target;
    public float maxAngleSpeed = 10f;
    public float angleAcceleration = 0.01f;
    private bool _isActive;
    private float _currentAngleSpeed = 0f;
    private float _deltaTime = 0f;

    public void Operate()
    {
        Debug.Log("Operate");
        _isActive = !_isActive;
        _deltaTime = 0f;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (maxAngleSpeed - _currentAngleSpeed > 0.1f)
            {
                _deltaTime += Time.deltaTime;
                if (_deltaTime >= 0.1f)
                {
                    _currentAngleSpeed += angleAcceleration;
                    _deltaTime = 0f;
                }
            }
            
            target.transform.Rotate(_currentAngleSpeed, 0, 0);
        }
        else
        {
            switch (_currentAngleSpeed)
            {
                case > 0f:
                {
                    _deltaTime += Time.deltaTime;
                    if (_deltaTime >= 0.1f)
                    {
                        _currentAngleSpeed -= angleAcceleration;
                        _deltaTime = 0f;
                    }

                    break;
                }
                case <= 0f:
                    _currentAngleSpeed = 0f;
                    break;
            }

            target.transform.Rotate(_currentAngleSpeed, 0, 0);
        }
    }
}

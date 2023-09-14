using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemniscateMovement : MonoBehaviour
{
    public float c = 1.0f;

    private readonly float _sqrtOfTwo = (float)Math.Sqrt(2);
    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        var t = Time.time;
        
        var cos = Math.Cos(t);
        var sin = Math.Sin(t);
        
        var z = (float)((c * _sqrtOfTwo * cos)/(1 + Math.Pow(sin, 2)));
        var x = (float)(z * sin);

        var position = _initialPosition + new Vector3(x, 0, z);

        transform.position = position;
    }
}

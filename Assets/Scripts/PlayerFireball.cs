using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireball : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<ReactiveTarget>();
        if (target != null)
        {
            target.ReactToHit();
        }
    }
}

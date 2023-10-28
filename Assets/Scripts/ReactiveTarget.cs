﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public void ReactToHit()
    {
        var behavior = GetComponent<WanderingAI>();
        if(behavior != null )
        {
            behavior.SetAlive( false );
        }
        StartCoroutine(Die());
    }
    
    private IEnumerator Die()
    {
        UIController.EnemyKilled();
        
        transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
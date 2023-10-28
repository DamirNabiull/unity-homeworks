using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCharacter : MonoBehaviour
{
    private int _health;
    private void Start()
    {
        _health = 5;
        UIController.SetHealth(_health);
    }
    
    public void Hurt(int damage)
    {
        if (_health > 0)
        {
            _health -= damage;
            Debug.Log($"Health: {_health}");
        
            UIController.SetHealth(_health);
        }
    }
}
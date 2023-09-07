using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector3 position;

    private int _health = 100;
    private float _lastUpdate;

    private static readonly System.Random Rand = new System.Random();

    private void UpdateHealth(int delta)
    {
        if (_health < 0)
        {
            return;
        }
        
        _health += delta;
            
        if (_health <= 0)
        {
            Debug.Log("player died");
        }
    }

    private void SetRandomPosition()
    {
        var x = (float)Rand.NextDouble() * (Rand.Next() % 100);
        var y = (float)Rand.NextDouble() * (Rand.Next() % 100);
        var z = (float)Rand.NextDouble() * (Rand.Next() % 100);
        UpdatePosition(x, y, z);
    }
    
    private void UpdatePosition(float x, float y, float z)
    {
        position = new Vector3(x, y, z);
    }

    private void Start()
    {
        _lastUpdate = Time.time;
    }

    private void Update()
    {
        if (Time.time - _lastUpdate < 2f) return;
        
        _lastUpdate = Time.time;
        UpdateHealth(-Rand.Next(10, 100));
        SetRandomPosition();
    }
}

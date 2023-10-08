using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] targets;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter:");
        Debug.Log(other.name);
        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit:");
        Debug.Log(other.name);
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpSpeed = 20f;
    
    private void OnTriggerEnter(Collider other)
    {
        var charController = other.GetComponent<CharacterController>();
        if (other == null) return;
        
        var movement = other.transform.TransformDirection(new Vector3(0f, jumpSpeed, 0f)); 
        charController.Move(movement);
    }
}

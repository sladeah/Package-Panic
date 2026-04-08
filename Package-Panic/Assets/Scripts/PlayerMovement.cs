using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 7f;

    private void Update()
    {
        if (!IsOwner) return; 

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }
}
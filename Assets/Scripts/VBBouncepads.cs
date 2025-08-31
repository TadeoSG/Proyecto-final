using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBBouncepads : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    
    float highestPoint;

    Rigidbody rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
     {
        if (!playerController.isGrounded)
        {
            if (player.transform.position.y > highestPoint)
            {
                highestPoint = player.transform.position.y;
            }
        }
        else
        {
            highestPoint = player.transform.position.y;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == player)
        {
            rb.velocity = Vector3.zero;

            Vector3 bounceDirection = transform.up;
            
            float bounceStrength = Mathf.Abs(highestPoint - transform.position.y);
            rb.AddForce(bounceDirection * bounceStrength, ForceMode.Impulse);

            playerController.isGrounded = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepads : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    [SerializeField] float bounceForce = 10f;
    public AudioSource BoingSound; // drag your AudioSource here in Inspector

    Rigidbody rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        playerController = player.GetComponent<PlayerController>(); // 👈 IMPORTANTE
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector3.zero;

            Vector3 bounceDirection = transform.up;
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);

            if (BoingSound != null)
            {
                BoingSound.Play();
            }

            // Ahora sí funciona
            playerController.isGrounded = false;
        }
    }
}

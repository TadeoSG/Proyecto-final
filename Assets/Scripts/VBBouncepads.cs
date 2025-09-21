using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBBouncepads : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] GameObject player;
    [SerializeField] float bounceLoss = 0.5f; // 0 = rebote perfecto, 0.5 = pierde mitad de energía
    float highestPoint;
    public AudioSource BoingSound; // drag your AudioSource here in Inspector

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
            if (BoingSound != null)
            {
                BoingSound.Play();
            }
            // Distancia de caída
            float fallDistance = highestPoint - transform.position.y;
            if (fallDistance <= 0) return; // por si algo raro

            // Usamos fórmula física: v = sqrt(2 * g * h)
            float fallSpeed = Mathf.Sqrt(2f * Physics.gravity.magnitude * fallDistance);

            // Rebote proporcional a la velocidad de caída
            float bounceStrength = fallSpeed * (1f - bounceLoss);

            // Reset de velocidad antes del impulso
            rb.velocity = Vector3.zero;

            // Rebote hacia arriba
            rb.AddForce(transform.up * bounceStrength, ForceMode.Impulse);

            playerController.isGrounded = false;
        }
    }
}

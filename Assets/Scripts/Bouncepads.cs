using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncepads : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float bounceForce = 10f;

    Rigidbody rb; // <-- Declarar aquí

    void Start()
    {
        // Obtener el Rigidbody del jugador al iniciar
        rb = player.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision col)
    {
        // Solo aplicar la fuerza si el objeto que colisiona es el player
        if (col.gameObject == player)
        {
            // Opcional: limpiar velocidad previa
            rb.velocity = Vector3.zero;

            Vector3 bounceDirection = transform.up;

            // Impulsar hacia arriba
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            
        }
    }
}

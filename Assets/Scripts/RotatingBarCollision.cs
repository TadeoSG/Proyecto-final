using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBarCollision : MonoBehaviour
{
    [SerializeField] private float pushForce = 15f;
    [SerializeField] private bool rotateClockwise = true;
    [SerializeField] private float upwardBoost = 5f; // 👈 fuerza extra hacia arriba
    public AudioSource BoingSound; // drag your AudioSource here in Inspector


    private Transform barAxis;

    void Start()
    {
        // el centro de rotación de la barra
        barAxis = transform.root; 
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (BoingSound != null)
            {
                BoingSound.Play();
            }
            Rigidbody rb = col.rigidbody;
            if (rb == null) return;

            ContactPoint contact = col.contacts[0];
            Vector3 radius = contact.point - barAxis.position;

            // dirección tangencial al giro
            Vector3 tangentialDir = Vector3.Cross(Vector3.up, radius).normalized;
            if (!rotateClockwise)
                tangentialDir = -tangentialDir;

            // agregamos un poco de "hacia arriba" para que nunca empuje al suelo
            Vector3 finalDir = (tangentialDir + Vector3.up * 0.3f).normalized;

            rb.velocity = Vector3.zero;
            rb.AddForce(finalDir * pushForce + Vector3.up * upwardBoost, ForceMode.Impulse);
        }
    }
}

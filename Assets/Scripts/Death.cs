using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public AudioSource buttonSound; // drag your AudioSource here in Inspector
    [SerializeField] GameObject deathBarrier;
    GameObject lastCheckpoint;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckpoint = collision.gameObject;
        }
        else if (collision.gameObject == deathBarrier)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = (lastCheckpoint.transform.position + new Vector3(0, 2, 0));
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void OnRespawnButton()
    {
        Respawn();
        AudioManager.instance.PlayButtonSound();

    }
}




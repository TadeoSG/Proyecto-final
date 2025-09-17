using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
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
            transform.position = (lastCheckpoint.transform.position + new Vector3(0, 2, 0));
        }
    }
}

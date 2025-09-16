using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Solo si quieres cambiar de escena

public class WinPlatform : MonoBehaviour
{
    public bool hasWon = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasWon = true;

        }
    }
}
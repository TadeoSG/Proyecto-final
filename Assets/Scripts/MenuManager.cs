using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public AudioSource buttonSound; // drag your AudioSource here in Inspector
    // Start is called before the first frame update
    public void OnPlayButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level Manager");
        AudioManager.instance.PlayButtonSound();

    }
    public void OnExitButtonClick()
    {
        AudioManager.instance.PlayButtonSound();

        Application.Quit();
    }
}

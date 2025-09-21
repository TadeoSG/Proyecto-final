using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public AudioSource buttonSound; // drag your AudioSource here in Inspector

    // Start is called before the first frame update
    public void OnLVL1ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        AudioManager.instance.PlayButtonSound();

    }
    public void OnLVL2ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
        AudioManager.instance.PlayButtonSound();

    }
    public void OnLVL3ButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 3");
        AudioManager.instance.PlayButtonSound();
    }
    public void OnBackButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        AudioManager.instance.PlayButtonSound();

    }
}

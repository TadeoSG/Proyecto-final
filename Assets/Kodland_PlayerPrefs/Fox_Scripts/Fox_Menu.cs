using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fox_Menu : MonoBehaviour
{
    // Una referencia al botón Cargar juego
    [SerializeField] Button loadButton;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        // Comprobamos si tenemos un espacio para guardar. Si lo tenemos, activamos el botón Cargar
        if (PlayerPrefs.HasKey("posX"))
        {
            loadButton.interactable = true;
        }
    }
    // La nueva función del juego
    public void StartNewGame()
    {
        // Comprobamos si tenemos un espacio para guardar. Si lo tenemos, borramos todos los espacios para guardar y comenzamos un nuevo juego.
        if(PlayerPrefs.HasKey("posX"))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Game");
        }
        // De lo contrario, simplemente comenzamos el juego.
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
    // Una función que inicia el juego con todos los datos guardados.
    public void LoadGame()
    {
        // Iniciar el juego si tenemos espacios para guardar
        if (PlayerPrefs.HasKey("posX"))
        {
            SceneManager.LoadScene("Game");
        }
    }
}

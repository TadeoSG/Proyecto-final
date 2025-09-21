using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMenuManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI GoldPaused;
    public TextMeshProUGUI GoldWon;
    public TextMeshProUGUI SilverPaused;
    public TextMeshProUGUI SilverWon;
    public TextMeshProUGUI BronzePaused;
    public TextMeshProUGUI BronzeWon;
    public TextMeshProUGUI YourTime;
    [SerializeField] private GameObject pauseOverlay; 
    [SerializeField] private GameObject PausedMenu;
    [SerializeField] private GameObject WinningMenu;
    public AudioSource buttonSound; // drag your AudioSource here in Inspector
    

    private WinPlatform winPlatform;
    private Timer timer; 

    // 📌 referencia al script de cámara
    private FirstPerson firstPerson;

    // Estado de victoria
    private bool isWin = false;

    void Start()
    {
        pauseOverlay.SetActive(false);
        PausedMenu.SetActive(false);
        WinningMenu.SetActive(false);

        winPlatform = FindObjectOfType<WinPlatform>();
        timer = FindObjectOfType<Timer>();
        firstPerson = FindObjectOfType<FirstPerson>();
    }

    void Update()
    {
        // --- Victoria ---
        if (!isWin && winPlatform != null && winPlatform.hasWon)
        {
            WinGame();
            winPlatform.hasWon = false; // evitar repetir
        }

        // --- Pausa con Escape ---
        if (!isWin && Input.GetKeyDown(KeyCode.Escape)) // 👈 Solo si NO has ganado
        {
            if (pauseOverlay.activeSelf && PausedMenu.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // --- Botones ---
    public void OnExitButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level Manager");
        AudioManager.instance.PlayButtonSound();

    }

    public void OnResumeButtonClick()
    {
        ResumeGame();
        AudioManager.instance.PlayButtonSound();

    }

    // --- Pausa ---
    private void PauseGame()
    {
        Time.timeScale = 0f;
        pauseOverlay.SetActive(true);
        PausedMenu.SetActive(true);

        if (firstPerson != null) firstPerson.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        LevelTrophyRequirements();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseOverlay.SetActive(false);
        PausedMenu.SetActive(false);

        if (firstPerson != null) firstPerson.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // --- Victoria ---
    private void WinGame()
    {
        isWin = true;
        Time.timeScale = 0f;

        WinningMenu.SetActive(true);
        pauseOverlay.SetActive(true);

        LevelTrophyRequirements();

        if (timer != null)
        {
            YourTime.text = "Your Time: " + timer.Runtime;
        }

        if (firstPerson != null) firstPerson.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // --- Requisitos de Trofeos ---
    public void LevelTrophyRequirements()
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        if (currentLevel == "Tutorial")
        {
            GoldPaused.text = "Gold: <= 20s";
            GoldWon.text = "Gold: <= 20s";
            SilverPaused.text = "Silver: <= 30s";
            SilverWon.text = "Silver: <= 30s";
            BronzePaused.text = "Bronze: <= 45s";
            BronzeWon.text = "Bronze: <= 45s";
        }
        else if (currentLevel == "Level 2")
        {
            GoldPaused.text = "Gold: <= 40s";
            GoldWon.text = "Gold: <= 40s";
            SilverPaused.text = "Silver: <= 60s";
            SilverWon.text = "Silver: <= 60s";
            BronzePaused.text = "Bronze: <= 90s";
            BronzeWon.text = "Bronze: <= 90s";
        }
        else if (currentLevel == "Level 3")
        {
            GoldPaused.text = "Gold: <= 1:20";
            GoldWon.text = "Gold: <= 1:20";
            SilverPaused.text = "Silver: <= 1:50";
            SilverWon.text = "Silver: <= 1:50";
            BronzePaused.text = "Bronze: <= 2:30";
            BronzeWon.text = "Bronze: <= 2:30";
        }
        else
        {
            GoldPaused.text = "Gold: N/A";
            GoldWon.text = "Gold: N/A";
            SilverPaused.text = "Silver: N/A";
            SilverWon.text = "Silver: N/A";
            BronzePaused.text = "Bronze: N/A";
            BronzeWon.text = "Bronze: N/A";
        }
    }
}

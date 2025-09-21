using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public bool running = false;

    private float elapsedTime = 0f;
    public TextMeshProUGUI timerText;

    private WinPlatform winPlatform;

    [Header("RunTimeInfo")]
    public string Runtime;
    public string BestRuntime;

    void Start()
    {
        timerText.text = "00:00.000"; 
        winPlatform = FindObjectOfType<WinPlatform>();

        // Cargar mejor tiempo de la escena actual
        string currentLevel = SceneManager.GetActiveScene().name;
        string key = "BestTime_" + currentLevel;

        float bestTime = PlayerPrefs.GetFloat(key, float.MaxValue);
        if (bestTime < float.MaxValue)
            BestRuntime = FormatTime(bestTime);
        else
            BestRuntime = "99:59.999";
    }

    void Update()
    {
        if (!winPlatform.hasWon && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) 
            || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            running = true;
        }

        if (running)
        {
            Tick();
        }

        if (winPlatform.hasWon)
        {
            Runtime = timerText.text;

            string currentLevel = SceneManager.GetActiveScene().name;
            string key = "BestTime_" + currentLevel;

            float bestTime = PlayerPrefs.GetFloat(key, float.MaxValue);

            // si es mejor tiempo, lo guardamos
            if (elapsedTime < bestTime)
            {
                PlayerPrefs.SetFloat(key, elapsedTime);
                PlayerPrefs.Save();
                BestRuntime = FormatTime(elapsedTime);
            }

            running = false;
        }
    }

    void Tick()
    {
        elapsedTime += Time.deltaTime;

        timerText.text = FormatTime(elapsedTime);
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}

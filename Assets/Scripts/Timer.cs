using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public bool running = false;

    private float elapsedTime = 0f; // store total time in seconds
    public TextMeshProUGUI timerText;

    private WinPlatform winPlatform;

    [Header("RunTimeInfo")]
    public string Runtime;
    public string BestRuntime;


    void Start()
    {
        timerText.text = "00:00.000"; // Initial text
        winPlatform = FindObjectOfType<WinPlatform>();
    }
    void Update()
    {
        if (winPlatform.hasWon == false && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            running = true;
        }

        if (running)
        {
            Tick();
        }

        if (winPlatform.hasWon == true)
        {
            Runtime = timerText.text;
            BestRuntime = PlayerPrefs.GetString("BestTime", "99:99.999");
            if (Runtime.CompareTo(BestRuntime) < 0)
            {
                PlayerPrefs.SetString("BestTime", Runtime);
            }
            running = false;
        }
    }

    void Tick()
    {
        elapsedTime += Time.deltaTime;

        // Break it down into minutes, seconds, and milliseconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        // Update the text → 00:00.000 format
        timerText.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BestTimeManager : MonoBehaviour
{
    [Header("Level Manager Best Times")]
    public TextMeshProUGUI Level1BestTimeText;
    public TextMeshProUGUI Level2BestTimeText;
    public TextMeshProUGUI Level3BestTimeText;

    [Header("Level Trophies")]
    public Image Level1Trophy;
    public Image Level2Trophy;
    public Image Level3Trophy;

    [Header("Trophy Sprites")]
    public Sprite No_Trophy;
    public Sprite Bronze_Trophy;
    public Sprite Silver_Trophy;
    public Sprite Gold_Trophy;

    void Start()
    {
        GetBestTime();
    }

    void GetBestTime()
    {
        // --- Nivel 1 ---
        float time1 = PlayerPrefs.GetFloat("BestTime_Tutorial", float.MaxValue);
        Level1BestTimeText.text = time1 == float.MaxValue ? "N/A" : FormatTime(time1);

        if (time1 == float.MaxValue) Level1Trophy.sprite = No_Trophy;
        else if (time1 <= 20f) Level1Trophy.sprite = Gold_Trophy;
        else if (time1 <= 30f) Level1Trophy.sprite = Silver_Trophy;
        else if (time1 <= 45f) Level1Trophy.sprite = Bronze_Trophy;
        else Level1Trophy.sprite = No_Trophy;

        // --- Nivel 2 ---
        float time2 = PlayerPrefs.GetFloat("BestTime_Level 2", float.MaxValue);
        Level2BestTimeText.text = time2 == float.MaxValue ? "N/A" : FormatTime(time2);

        if (time2 == float.MaxValue) Level2Trophy.sprite = No_Trophy;
        else if (time2 <= 40f) Level2Trophy.sprite = Gold_Trophy;
        else if (time2 <= 60f) Level2Trophy.sprite = Silver_Trophy;
        else if (time2 <= 90f) Level2Trophy.sprite = Bronze_Trophy;
        else Level2Trophy.sprite = No_Trophy;

        // --- Nivel 3 ---
        float time3 = PlayerPrefs.GetFloat("BestTime_Level 3", float.MaxValue);
        Level3BestTimeText.text = time3 == float.MaxValue ? "N/A" : FormatTime(time3);

        if (time3 == float.MaxValue) Level3Trophy.sprite = No_Trophy;
        else if (time3 <= 80f) Level3Trophy.sprite = Gold_Trophy;
        else if (time3 <= 110f) Level3Trophy.sprite = Silver_Trophy;
        else if (time3 <= 150f) Level3Trophy.sprite = Bronze_Trophy;
        else Level3Trophy.sprite = No_Trophy;
    }

    // Convierte segundos (float) a "MM:SS.mmm"
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}

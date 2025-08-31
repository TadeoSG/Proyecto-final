using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_Coin : MonoBehaviour
{
    Fox_Logic foxLogic;
    public string objectName;
    public bool isTaken;
    void Start()
    {
        foxLogic = FindObjectOfType<Fox_Logic>();
        if (PlayerPrefs.HasKey(objectName))
        {
            isTaken = PlayerPrefs.GetInt(objectName) == 1;
            gameObject.SetActive(!isTaken);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTaken = true;
            PlayerPrefs.SetInt(objectName, 1);
            gameObject.SetActive(false);

            var value = PlayerPrefs.GetInt("Coins", 0);
            PlayerPrefs.SetInt("Coins", value + 1);
            foxLogic.Coins();
        }
    }
}

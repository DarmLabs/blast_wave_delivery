using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class globalVariables : MonoBehaviour
{
    //Other variables
    Text universalCoin;
    //Save Variables
    public bool tutoActive;
    public int monedas;
    public bool firstTimePassed;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        universalCoin = GameObject.Find("UniversalCoins").GetComponent<Text>();
        LoadPlayer();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        tutoActive = data.tutoScreen;
        monedas = data.monedas;
        universalCoin.text = monedas.ToString();
        firstTimePassed = data.firstTimePassed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalVariables : MonoBehaviour
{
    public bool tutoActive;
    public int monedas;
    public bool firstTimePassed;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
    }
}

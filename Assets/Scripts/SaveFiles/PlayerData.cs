using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int monedas;
    public bool tutoScreen;
    public bool firstTimePassed;

    public void Data(globalVariables globalVariables)
    {
        monedas = globalVariables.monedas;
        tutoScreen = globalVariables.tutoActive;
        firstTimePassed = globalVariables.firstTimePassed;
    }
}

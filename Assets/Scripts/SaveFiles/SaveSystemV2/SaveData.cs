using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set
        {
            if(value != null)
            {
                _current = value;
            }
        }
    }

    public bool tutoActive;
    public int monedas;
    public bool firstTimePassed;
    //Tienda variables
        //Potenciadores
    public bool laser;
    public bool stopTime;
    public bool magnetic;
    public bool x2;
        //Objetos
    public int extraVida;
    public int deposit;
    public int inmune;
    public int cool;
    public int check;
    public bool slot;
        //Cosmeticos
    public bool skinInferno;
    public bool skinRadiactive;
    public bool skinLight;
    public bool skinRetro;
}

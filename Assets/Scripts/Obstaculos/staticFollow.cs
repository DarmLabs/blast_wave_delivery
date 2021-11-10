using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticFollow : MonoBehaviour
{
    //Este script es para que un objecto vaya a la misma velocidad en z que la moto pero quede estatico en x e y
    //public GameObject Moto;
    void Update()
    {
        //transform.Translate(0,0, Moto.GetComponent<playerMovement>().speedz * (Time.deltaTime * Moto.GetComponent<playerMovement>().speedMultiplier));
        transform.Translate(0,0, playerMovement.speedz * (Time.deltaTime * playerMovement.speedMultiplier));
    }
}

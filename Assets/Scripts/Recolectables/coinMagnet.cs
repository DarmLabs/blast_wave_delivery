using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinMagnet : MonoBehaviour
{
    public GameObject Moto;
    playerProprieties playerProprieties;
    Transform magnet;
    Vector3 radio;
    void Start()
    {
        Moto = GameObject.FindGameObjectWithTag("Player");
        playerProprieties = Moto.GetComponent<playerProprieties>();
        radio = new Vector3(10,0,10);
    }
    void FixedUpdate()
    {
        if(playerProprieties.magneticMode)
        {
            magnet = Moto.GetComponent<Transform>().GetChild(0);
            if(Vector3.Distance(transform.position, magnet.position) < 25f)
            {
                transform.position = Vector3.MoveTowards(transform.position, magnet.position, 3);
            }
            
        }
    }

}

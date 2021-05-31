using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerProprieties : MonoBehaviour
{
    float currentFuel = 100; //Cantidad de combustible que posee la moto en el momento
    int maxFuel = 100; //Cantidad de combustible maximo que la moto puede tener
    float emptyFuel; //Cantidad de combustible faltante para llegar al maximo
    float currentTime = 90; //Tiempo en el momento (Usar esto para manejar propiedades con tiempo)
    float minutes; //Display de minutos en el texto
    float seconds; //Display de segundos en el texto
    public Text timerText;
    public Text fuelText;
    void Update()
    {
        //Contador de tiempo y reinicia escena si llega a 0
        if(currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //Contador de combustible y reinicia escena si llega a 0
        if(currentFuel >= 0)
        {
            currentFuel -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        DisplayTime(currentTime);
        DisplayFuel();
    }
    void DisplayTime(float timeToDisplay) //Formato para el display del timer
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void OnTriggerEnter(Collider other) //Trigger para la deteccion de recolectables
    {
        if(other.gameObject.tag == "Fuel") //Detecto si es fuel
        {
            emptyFuel = maxFuel - currentFuel; //Obtengo cuanta fuel falta para llenar el tanque
            if(emptyFuel < 20) //Si el fuel que falta para llenar el tanque en menor a 20
            {
                currentFuel = currentFuel + emptyFuel; //Aplico la cantidad que falta para llenar el tanque para que no sobrepase el maximo
            }
            else
            {
                currentFuel = currentFuel + 20; //Aplico 20 de fuel
            }
            Destroy(other.gameObject); //Destruyo el recolectable
        }
        if(other.gameObject.tag == "Time") //Detecto si es tiempo
        {
            currentTime = currentTime + 20;
            Destroy(other.gameObject);
        }
    }
    void DisplayFuel()
    {
        fuelText.text = currentFuel.ToString("f0");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerProprieties : MonoBehaviour
{
    public float currentFuel = 200; //Cantidad de combustible que posee la moto en el momento
    int maxFuel = 200; //Cantidad de combustible maximo que la moto puede tener
    int currentPizzas;
    int generalPizzas;
    float emptyFuel; //Cantidad de combustible faltante para llegar al maximo
    float fuelConsumption;
    public float currentTime = 90; //Tiempo en el momento (Usar esto para manejar propiedades con tiempo)
    public string vehicleType;
    public int generalLife = 3;
    public Camera cam;
    public GameObject UI_Manager;
    Gameplay_Manager gameplay_Manager;
    void Start()
    {
        gameplay_Manager = UI_Manager.GetComponent<Gameplay_Manager>();
        vehicleStandard();
    }
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
            currentFuel -= Time.deltaTime * fuelConsumption;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
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
        if(other.gameObject.tag == "obs")
        {
            Destroy(other.gameObject);
            vehicleLifeCondition();
        }
        if(other.gameObject.tag == "Pizza")
        {
            currentPizzas = currentPizzas + 1;
        }
        if(other.gameObject.tag == "Checkpoint")
        {
            generalPizzas = generalPizzas + currentPizzas;
            currentPizzas = 0;
            generalLife = generalLife + 1;
            gameplay_Manager.LifeAdd();
        }
    }
    void vehicleLifeCondition()
    {
        generalLife = generalLife -1;
        gameplay_Manager.LifeDiscount();
        if(generalLife == 0)
        {
            gameplay_Manager.GameOver();
        }
    }
    public void vehicleStandard()
    {
        vehicleType = "Standard";
        fuelConsumption = 1;
        gameObject.GetComponent<playerMovement>().speedMultiplier = 1;
        gameObject.GetComponent<playerMovement>().HMultiplier = 1;
        cam.fieldOfView = 75;
        gameplay_Manager.vehicleButtonStandard();
    }
    public void vehicleFast()
    {
        vehicleType = "Fast";
        fuelConsumption = 1.5f;
        gameObject.GetComponent<playerMovement>().speedMultiplier = 1.5f;
        gameObject.GetComponent<playerMovement>().HMultiplier = 1;
        cam.fieldOfView = 85;
        gameplay_Manager.vehicleButtonFast();
    }
    public void vehicleTank()
    {
        vehicleType = "Tank";
        fuelConsumption = 0.5f;
        gameObject.GetComponent<playerMovement>().speedMultiplier = 1f;
        gameObject.GetComponent<playerMovement>().HMultiplier = 0.8f;
        cam.fieldOfView = 75;
        gameplay_Manager.vehicleButtonTank();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerProprieties : MonoBehaviour
{
    public float currentFuel = 300; //Cantidad de combustible que posee la moto en el momento
    //int maxFuel = 300; //Cantidad de combustible maximo que la moto puede tener
    public int currentPizzas;
    public int generalPizzas;
    float emptyFuel; //Cantidad de combustible faltante para llegar al maximo
    float fuelConsumption;
    public float currentTime = 90; //Tiempo en el momento (Usar esto para manejar propiedades con tiempo)
    public string vehicleType;
    public int generalLife = 3;
    bool inmune;
    bool destructiveMode;
    public bool x2Mode;
    public bool magneticMode;
    public BoxCollider laserColl;
    public LineRenderer laser;
    public Camera cam;
    public GameObject UI_Manager;
    Gameplay_Manager gameplay_Manager;
    Animator MotoAnimator;
    void Start()
    {
        gameplay_Manager = UI_Manager.GetComponent<Gameplay_Manager>();
        MotoAnimator = GetComponentInChildren<Animator>();
        modeStandard();
    }
    void Update()
    {
        //Contador de tiempo y reinicia escena si llega a 0
        /*if(currentTime >= 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            gameplay_Manager.GameOver();
        }*/
        //Contador de combustible y reinicia escena si llega a 0
        if(currentFuel >= 0)
        {
            currentFuel -= Time.deltaTime * fuelConsumption;
        }
        else
        {
            gameplay_Manager.GameOver();
        }
        if(inmune && !destructiveMode && !laser.enabled)
        {
            MotoAnimator.SetBool("inmune", true);
        }
        else
        {
            MotoAnimator.SetBool("inmune", false);
        }
    }
    void OnTriggerEnter(Collider other) //Trigger para la deteccion de recolectables
    {
        if(other.gameObject.tag == "Fuel") //Detecto si es fuel
        {
            currentFuel = 300;
            /*
            emptyFuel = maxFuel - currentFuel; //Obtengo cuanta fuel falta para llenar el tanque
            if(emptyFuel < 20) //Si el fuel que falta para llenar el tanque en menor a 20
            {
                currentFuel = currentFuel + emptyFuel; //Aplico la cantidad que falta para llenar el tanque para que no sobrepase el maximo
            }
            else
            {
                currentFuel = currentFuel + 20; //Aplico 20 de fuel
            }*/
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
            if(destructiveMode)
            {
                if(x2Mode)
                {
                    currentPizzas = currentPizzas + 20;
                }
                else
                {
                    currentPizzas = currentPizzas + 10;
                }
            }
            if(!inmune)
            {
                vehicleLifeCondition();
            }
        }
        if(other.gameObject.tag == "Pizza")
        {
            if(x2Mode)
            {
                currentPizzas = currentPizzas + 2;
            }
            else
            {
                currentPizzas = currentPizzas + 1;
            }
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Checkpoint")
        {
            generalPizzas = generalPizzas + currentPizzas;
            currentPizzas = 0;
            if(generalLife != 2)
            {
                generalLife = generalLife + 1;
                gameplay_Manager.LifeChange();
            }
        }
    }
    void vehicleLifeCondition()
    {
        generalLife = generalLife -1;
        inmune = true;
        StartCoroutine(inmuneTime(2));
        if(generalLife == -1)
        {
            gameplay_Manager.GameOver();
        }
        gameplay_Manager.LifeChange();
    }
    IEnumerator inmuneTime(int secs)
    {
        yield return new WaitForSeconds(secs);
        inmune = false;
    }
    /*public void vehicleStandard()
    {
        vehicleType = "Standard";
        fuelConsumption = 2;
        gameObject.GetComponent<playerMovement>().speedMultiplier = 1;
        gameObject.GetComponent<playerMovement>().HMultiplier = 1;
        cam.fieldOfView = 75;
        gameplay_Manager.vehicleButtonStandard();
    }
    public void vehicleFast()
    {
        vehicleType = "Fast";
        fuelConsumption = 6f;
        gameObject.GetComponent<playerMovement>().speedMultiplier = 1.5f;
        gameObject.GetComponent<playerMovement>().HMultiplier = 1;
        cam.fieldOfView = 85;
        gameplay_Manager.vehicleButtonFast();
    }
    public void vehicleTank()
    {
        vehicleType = "Tank";
        fuelConsumption = 1.5f;
        gameObject.GetComponent<playerMovement>().speedMultiplier = 1f;
        gameObject.GetComponent<playerMovement>().HMultiplier = 0.8f;
        cam.fieldOfView = 75;
        gameplay_Manager.vehicleButtonTank();
    }*/
    public void modeFast()
    {
        destructiveMode = true;
        inmune = true;
        currentFuel = currentFuel -10;
        gameObject.GetComponent<playerMovement>().speedMultiplier = 5;
        gameObject.GetComponent<playerMovement>().HMultiplier = 0.2f;
        cam.fieldOfView = 90;
        gameplay_Manager.FastButton();
        StartCoroutine(clearModes(5));
    }
    public void modeTank()
    {
        destructiveMode = true;
        inmune = true;
        currentFuel = currentFuel -10;
        gameObject.GetComponent<playerMovement>().HMultiplier = 0.8f;
        gameplay_Manager.TankButton();
        StartCoroutine(clearModes(5));
    }
    public void modex2()
    {
        x2Mode = true;
        currentFuel = currentFuel -10;
        gameplay_Manager.x2Button();
        StartCoroutine(clearModes(15));
    }
    public void modeMagnetic()
    {
        magneticMode = true;
        currentFuel = currentFuel -10;
        gameplay_Manager.MagneticButton();
        StartCoroutine(clearModes(5));
    }
    public void modeLaser()
    {
        laserColl.enabled = true;
        laser.enabled = true;
        inmune = true;
        gameplay_Manager.LaserButton();
        StartCoroutine(clearModes(5));
    }
    public void modeStopTime()
    {
        Time.timeScale = .5f;
        cam.fieldOfView = 70;
        currentFuel = currentFuel -10;
        gameObject.GetComponent<playerMovement>().HMultiplier = 1.75f;
        gameplay_Manager.StopTimeButton();
        StartCoroutine(clearModes(5));
    }
    public void modeStandard()
    {
        gameObject.GetComponent<playerMovement>().speedMultiplier = 1;
        gameObject.GetComponent<playerMovement>().HMultiplier = 1;
        fuelConsumption = 2;
        cam.fieldOfView = 75;
        destructiveMode = false;
        inmune =false;
        x2Mode = false;
        laserColl.enabled = false;
        laser.enabled = false;
        magneticMode = false;
    }
    IEnumerator clearModes(int secs)
    {
        yield return new WaitForSeconds(secs);
        gameplay_Manager.clearMode();
        modeStandard();
        Time.timeScale = 1;
    }
}

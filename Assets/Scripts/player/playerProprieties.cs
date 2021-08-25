using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerProprieties : MonoBehaviour
{
    public float currentFuel = 200; //Cantidad de combustible que posee la moto en el momento
    int maxFuel = 200; //Cantidad de combustible maximo que la moto puede tener
    float emptyFuel; //Cantidad de combustible faltante para llegar al maximo
    float fuelConsumption;
    public float currentTime = 90; //Tiempo en el momento (Usar esto para manejar propiedades con tiempo)
    public string vehicleType;
    //public int playerLifeStandard;
    //public int playerLifeFast;
    //public int playerLifeTank;
    public int generalLife = 3;
    //bool standardLocked = false;
    //bool tankLocked = false;
    //bool fastLocked = false;
    Color32 activeColor = new Color32(148, 214, 255, 255);
    Color32 unactiveColor = new Color32(255, 255, 255, 255);
    public Color32 lockedColor = new Color32(106, 106, 106, 255);
    public Camera cam;
    public GameObject tunnelEffect;
    public AudioSource Sound1;
    public GameObject UI_Manager;
    Gameplay_Manager gameplay_Manager;
    void Start()
    {
        gameplay_Manager = UI_Manager.GetComponent<Gameplay_Manager>();
        vehicleButtonStandard();
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
            /*if(vehicleType == "Standard")
            {
                playerLifeStandard = playerLifeStandard - 1;
            }
            if(vehicleType == "Fast")
            {
                playerLifeFast = playerLifeFast - 1;
            }
            if(vehicleType == "Tank")
            {
                playerLifeTank = playerLifeTank - 1;
            }*/
            vehicleLifeCondition();
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
        /*
        if(playerLifeStandard == 0 && playerLifeFast == 0 && playerLifeTank == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(playerLifeStandard == 0 && !standardLocked)
        {
            standardLocked = true;
            SButton.GetComponent<Button>().enabled = false;
            SButton.GetComponent<Image>().color = lockedColor;
            if(playerLifeTank > 0)
            {
                vehicleButtonTank();
            }
            else
            {
                vehicleButtonFast();
            }
        }
        if(playerLifeTank == 0 && !tankLocked)
        {
            tankLocked = true;
            TButton.GetComponent<Button>().enabled = false;
            TButton.GetComponent<Image>().color = lockedColor;
            if(playerLifeFast > 0)
            {
                vehicleButtonFast();
            }
            else
            {
                vehicleButtonStandard();
            }
        }
        if(playerLifeFast == 0 && !fastLocked)
        {
            fastLocked = true;
            FButton.GetComponent<Button>().enabled = false;
            FButton.GetComponent<Image>().color = lockedColor;
            if(playerLifeStandard > 0)
            {
                vehicleButtonStandard();
            }
            else
            {
                vehicleButtonTank();
            }
        }
        */
    }
    public void vehicleButtonStandard()
    {
        vehicleType = "Standard";
        fuelConsumption = 1;
        gameplay_Manager.SButton.GetComponent<Image>().color = activeColor;
        gameObject.GetComponent<playerMovementHorizontal>().speedMultiplier = 1;
        gameObject.GetComponent<playerMovementHorizontal>().HMultiplier = 1;
        cam.fieldOfView = 75;
        tunnelEffect.GetComponent<Animator>().Play("StandardEffect");
        Sound1.pitch = 1f;

        if(gameplay_Manager.TButton.GetComponent<Image>().color != lockedColor)
        {
            gameplay_Manager.TButton.GetComponent<Image>().color = unactiveColor;
        }
        if(gameplay_Manager.FButton.GetComponent<Image>().color != lockedColor)
        {
            gameplay_Manager.FButton.GetComponent<Image>().color = unactiveColor;
        }
    }
    public void vehicleButtonFast()
    {
        vehicleType = "Fast";
        fuelConsumption = 1.5f;
        gameplay_Manager.FButton.GetComponent<Image>().color = activeColor;
        gameObject.GetComponent<playerMovementHorizontal>().speedMultiplier = 1.5f;
        gameObject.GetComponent<playerMovementHorizontal>().HMultiplier = 1;
        cam.fieldOfView = 85;
        tunnelEffect.GetComponent<Animator>().Play("FastEffect");
        Sound1.pitch = 1.25f;

        if(gameplay_Manager.TButton.GetComponent<Image>().color != lockedColor)
        {
            gameplay_Manager.TButton.GetComponent<Image>().color = unactiveColor;
        }
        if(gameplay_Manager.SButton.GetComponent<Image>().color != lockedColor)
        {
            gameplay_Manager.SButton.GetComponent<Image>().color = unactiveColor;
        }
    }
    public void vehicleButtonTank()
    {
        vehicleType = "Tank";
        fuelConsumption = 0.5f;
        gameObject.GetComponent<playerMovementHorizontal>().speedMultiplier = 1f;
        gameObject.GetComponent<playerMovementHorizontal>().HMultiplier = 0.8f;
        cam.fieldOfView = 75;
        tunnelEffect.GetComponent<Animator>().Play("TankEffect");
        Sound1.pitch = 0.75f;

        gameplay_Manager.TButton.GetComponent<Image>().color = activeColor;
        if(gameplay_Manager.SButton.GetComponent<Image>().color != lockedColor)
        {
            gameplay_Manager.SButton.GetComponent<Image>().color = unactiveColor;
        }
        if(gameplay_Manager.FButton.GetComponent<Image>().color != lockedColor)
        {
            gameplay_Manager.FButton.GetComponent<Image>().color = unactiveColor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerProprieties : MonoBehaviour
{
    float currentFuel = 200; //Cantidad de combustible que posee la moto en el momento
    int maxFuel = 200; //Cantidad de combustible maximo que la moto puede tener
    float emptyFuel; //Cantidad de combustible faltante para llegar al maximo
    float currentTime = 90; //Tiempo en el momento (Usar esto para manejar propiedades con tiempo)
    float minutes; //Display de minutos en el texto
    float seconds; //Display de segundos en el texto
    public Text timerText;
    public Text fuelText;
    string vehicleType;
    int playerLifeStandard;
    int playerLifeFast;
    int playerLifeTank;
    float fuelConsumption;
    bool standardLocked = false;
    bool tankLocked = false;
    bool fastLocked = false;
    public GameObject SButton;
    public GameObject FButton;
    public GameObject TButton;
    Color32 activeColor = new Color32(148, 214, 255, 255);
    Color32 unactiveColor = new Color32(255, 255, 255, 255);
    Color32 lockedColor = new Color32(106, 106, 106, 255);
    public Camera cam;
    public Animator tunnelEffect;
    public Text fpsDisplay;
    void Start()
    {
        vehicleButtonStandard();
    }
    void Update()
    {
        FPSCounter();
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
        DisplayTime(currentTime);
        DisplayFuel();
    }
    void FPSCounter()
    {
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = "" + fps;
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
        if(other.gameObject.tag == "Obstacle")
        {
            if(vehicleType == "Standard")
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
            }
            vehicleLifeCondition();
        }
    }
    void DisplayFuel()
    {
        fuelText.text = currentFuel.ToString("f0");
    }
    void vehicleLifeCondition()
    {
        if(playerLifeStandard == 0 && playerLifeFast == 0 && playerLifeTank == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(playerLifeStandard == 0 && !standardLocked)
        {
            standardLocked = true;
            SButton.GetComponent<Button>().enabled = false;
            if(playerLifeTank > 0)
            {
                vehicleType = "Tank";
            }
            else
            {
                vehicleType = "Fast";
            }
        }
        if(playerLifeTank == 0 && !tankLocked)
        {
            tankLocked = true;
            TButton.GetComponent<Button>().enabled = false;
            if(playerLifeFast > 0)
            {
                vehicleType = "Fast";
            }
            else
            {
                vehicleType = "Standard";
            }
        }
        if(playerLifeFast == 0 && !fastLocked)
        {
            fastLocked = true;
            FButton.GetComponent<Button>().enabled = false;
            if(playerLifeStandard > 0)
            {
                vehicleType = "Standard";
            }
            else
            {
                vehicleType = "Tank";
            }
        }

    }
    public void vehicleButtonStandard()
    {
        vehicleType = "Standard";
        fuelConsumption = 1;
        SButton.GetComponent<Image>().color = activeColor;
        gameObject.GetComponent<playerMovementHorizontal>().speedMultiplier = 1;
        cam.fieldOfView = 90;
        tunnelEffect.GetComponent<Animator>().Play("StandardEffect");

        if(TButton.GetComponent<Image>().color != lockedColor)
        {
            TButton.GetComponent<Image>().color = unactiveColor;
        }
        if(FButton.GetComponent<Image>().color != lockedColor)
        {
            FButton.GetComponent<Image>().color = unactiveColor;
        }
    }
    public void vehicleButtonFast()
    {
        vehicleType = "Fast";
        fuelConsumption = 1.5f;
        FButton.GetComponent<Image>().color = activeColor;
        gameObject.GetComponent<playerMovementHorizontal>().speedMultiplier = 1.5f;
        cam.fieldOfView = 100;
        tunnelEffect.GetComponent<Animator>().Play("FastEffect");

        if(TButton.GetComponent<Image>().color != lockedColor)
        {
            TButton.GetComponent<Image>().color = unactiveColor;
        }
        if(SButton.GetComponent<Image>().color != lockedColor)
        {
            SButton.GetComponent<Image>().color = unactiveColor;
        }
    }
    public void vehicleButtonTank()
    {
        vehicleType = "Tank";
        fuelConsumption = 0.5f;
        gameObject.GetComponent<playerMovementHorizontal>().speedMultiplier = 0.5f;
        cam.fieldOfView = 80;
        tunnelEffect.GetComponent<Animator>().Play("TankEffect");

        TButton.GetComponent<Image>().color = activeColor;
        if(SButton.GetComponent<Image>().color != lockedColor)
        {
            SButton.GetComponent<Image>().color = unactiveColor;
        }
        if(FButton.GetComponent<Image>().color != lockedColor)
        {
            FButton.GetComponent<Image>().color = unactiveColor;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerProprieties : MonoBehaviour
{
    public float currentFuel = 300; //Cantidad de combustible que posee la moto en el momento
    //int maxFuel = 300; //Cantidad de combustible maximo que la moto puede tener
    public int currentCoin;
    public int generalCoin;
    public int pizzas;
    float fuelConsumption;
    public float currentTime = 90; //Tiempo en el momento (Usar esto para manejar propiedades con tiempo)
    public int generalLife = 3;
    public bool CajaRecolectada = false;
    public bool inmune;
    bool destructiveMode;
    bool inmuneObj;
    bool x2Mode;
    public bool magneticMode;
    public GameObject laser;
    public Camera cam;
    public GameObject UI_Manager;
    Gameplay_Manager gameplay_Manager;
    Animator MotoAnimator;
    public GameObject spawnManager;
    GameObject MotoModel;
    GameObject PjModel;
    void Start()
    {
        gameplay_Manager = UI_Manager.GetComponent<Gameplay_Manager>();
        MotoAnimator = GetComponentInChildren<Animator>();
        MotoModel = transform.GetChild(1).gameObject;
        PjModel = MotoModel.transform.GetChild(0).gameObject;
        modeStandard();
        gameplay_Manager.ExtraLifeChecker();
        SkinChecker();
    }
    void Update()
    {
        //Contador de combustible y reinicia escena si llega a 0
        if(currentFuel >= 0)
        {
            currentFuel -= Time.deltaTime * fuelConsumption;
        }
        else
        {
            gameplay_Manager.GameOver();
        }
    }
    void SkinChecker()
    {
        switch(SaveData.current.actualSkin)
        {
            case "INICIAL":
                MotoModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Moto_Mat_1");
                PjModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Traje_Mat_1");
                break;
            case "INFERNO":
                MotoModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Moto_Mat_2");
                PjModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Traje_Mat_2");
                break;
            case "RADIACTIVO":
                MotoModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Moto_Mat_3");
                PjModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Traje_Mat_3");
                break;
            case "LIGHT":
                MotoModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Moto_Mat_4");
                PjModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Traje_Mat_4");
                break;
            case "RETRO":
                MotoModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Moto_Mat_5");
                PjModel.GetComponent<Renderer>().material = Resources.Load<Material>("Skins/Traje_Mat_5");
                break;
        }
    }
    void OnTriggerEnter(Collider other) //Trigger para la deteccion de recolectables
    {
        if(other.gameObject.tag == "Fuel") //Detecto si es fuel
        {
            currentFuel = 300;
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
                    currentCoin = currentCoin + 20;
                }
                else
                {
                    currentCoin = currentCoin + 10;
                }
            }
            if(!inmune && !destructiveMode && !laser.activeSelf && !inmuneObj)
            {
                MotoAnimator.SetBool("inmune", true);
                vehicleLifeCondition();
            }
            else
            {
                MotoAnimator.SetBool("inmune", false);
            }
        }
        if(other.gameObject.tag == "Coin")
        {
            if(gameplay_Manager.audioController != null)
            {
                gameplay_Manager.audioController.PlayCoinSFX();
            }
            if(x2Mode)
            {
                currentCoin = currentCoin + 2;
            }
            else
            {
                currentCoin = currentCoin + 1;
            }
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Checkpoint")
        {
            generalCoin = generalCoin + currentCoin;
            currentCoin = 0;
            if(generalLife != 2)
            {
                generalLife = generalLife + 1;
                gameplay_Manager.LifeChange();
            }
            TileManager.checkActivo = false;
        }
        if(other.gameObject.tag == "Caja")
        {
            spawnManager.GetComponent<spawnManager>().CajaExistente = false;
            CajaRecolectada = true;
            pizzas = pizzas +1;
            Destroy(other.gameObject);
        }
    }
    void vehicleLifeCondition()
    {
        generalLife = generalLife -1;
        inmune = true;
        gameplay_Manager.InmuneChecker();
        StartCoroutine(inmuneTime(2));
        if(generalLife == -1)
        {
            gameplay_Manager.GameOver();
            generalLife = 0;
        }
        gameplay_Manager.LifeChange();
    }
    IEnumerator inmuneTime(int secs)
    {
        yield return new WaitForSeconds(secs);
        inmune = false;
        gameplay_Manager.InmuneChecker();
    }
    public void modeFast()
    {
        destructiveMode = true;
        inmune = true;
        gameplay_Manager.InmuneChecker();
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
        gameplay_Manager.InmuneChecker();
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
        laser.SetActive(true);
        inmune = true;
        gameplay_Manager.InmuneChecker();
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
        gameplay_Manager.InmuneChecker();
        x2Mode = false;
        laser.SetActive(false);
        magneticMode = false;
    }
    IEnumerator clearModes(int secs)
    {
        yield return new WaitForSeconds(secs);
        gameplay_Manager.clearMode();
        modeStandard();
        Time.timeScale = 1;
    }
    //Obj Affections
    public void ExtraLife()
    {
        generalLife =+ 1;
        SaveData.current.extraVida -=1;
        gameplay_Manager.ExtraLifeChecker();
    }
    public void Desposit()
    {
        currentFuel = 300;
        SaveData.current.deposit -= 1;
        gameplay_Manager.DepositChecker();
    }
    public void Inmune()
    {
        inmune = true;
        inmuneObj = true;
        SaveData.current.inmune -= 1;
        gameplay_Manager.InmuneChecker();
    }
}

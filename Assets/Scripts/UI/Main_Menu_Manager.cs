using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Main_Menu_Manager : MonoBehaviour
{
    public Text[] universalCoins;
    public GameObject firstScreen;
    public GameObject shopScreen;
    public GameObject motoSection;
    public GameObject pjSection;
    public GameObject musicSection;
    public GameObject modeScreen;
    GameObject previousTextMode;
    bool firstTime = true;
    public GameObject endlessTexts;
    public GameObject endlessButton;
    public GameObject chillTexts;
    public GameObject chillButton;
    string buttonPressed;
    int selectedMode;
    public GameObject Main;
    GameObject slider;
    bool music;
    bool sfx;
    //Tienda
    public Text descriptionPot;
    public GameObject buyButtonPot;
    public Text descriptionObj;
    public GameObject buyButtonObj;    
    public Text confirmationText;
    public GameObject confirmationScreen;
    string itemName;
    int value;
    public Text[] objText;
    public GameObject[] blockers;
    public GameObject coinPanel;
    GameObject previousScreen;
    void Start()
    {
        OnLoadGame();   
        Time.timeScale = 1;
        selectMode();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SaveData.current.monedas += 1000;
            OnSaveGame();
            OnLoadGame();
        }
    }
    void DisplayUniversalCoins()
    {
        foreach (var item in universalCoins)
        {
            item.text = SaveData.current.monedas.ToString();
        }
    }
    void OnLoadGame()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/player.save");
        DisplayUniversalCoins();
        DisplayObjetos();
    }
    void OnSaveGame()
    {
        SerializationManager.Save(SaveData.current);
    }
    public void setOffFirstScreen()
    {
        firstScreen.SetActive(false);
    }
    public void ModesButton()
    {
        if(modeScreen.activeSelf == false)
        {
            modeScreen.SetActive(true);
            Main.SetActive(false);
        }
        else
        {
            modeScreen.SetActive(false);
            Main.SetActive(true);
        }
    }
    public void navigateMode()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        if(buttonPressed == "RightArrow" && selectedMode != 1)
        {
            selectedMode = selectedMode + 1;
        }
        if(buttonPressed == "LeftArrow" && selectedMode != 0)
        {
            selectedMode = selectedMode - 1;
        }
        selectMode();
    }
    public void selectMode()
    {
        if(selectedMode == 0)
        {
            if(firstTime == false)
            {
                previousTextMode.SetActive(false);
            }
            firstTime = false;
            endlessTexts.SetActive(true);
            endlessButton.transform.SetSiblingIndex(1);
            previousTextMode = endlessTexts;
        }
        if(selectedMode == 1)
        {
            previousTextMode.SetActive(false);
            chillTexts.SetActive(true);
            chillButton.transform.SetSiblingIndex(1);
            previousTextMode = chillTexts;
        }
    }
    public void LoadMode()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(buttonPressed);
    }
    public void ShopButton()
    {
        if(shopScreen.activeSelf == false)
        {
            shopScreen.SetActive(true);
            Main.SetActive(false);
        }
        else
        {
            shopScreen.SetActive(false);
            Main.SetActive(true);
        }
    }
    public void ShopSections()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        if(buttonPressed == "Potenciadores")
        {
            motoSection.transform.SetSiblingIndex(2);
            pjSection.transform.SetSiblingIndex(1);
            musicSection.transform.SetSiblingIndex(0);
        }
        if(buttonPressed == "Objetos")
        {
            pjSection.transform.SetSiblingIndex(2);
        }
        if(buttonPressed == "Cosmetics")
        {
            musicSection.transform.SetSiblingIndex(2);
            pjSection.transform.SetSiblingIndex(1);
            motoSection.transform.SetSiblingIndex(0);
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    public void MusicButton()
    {
        slider = GameObject.Find("MusicSlider");
        if(!music)
        {
            music = true;
            slider.GetComponent<Animator>().Play("Desliz");
        }
        else
        {
            music = false;
            slider.GetComponent<Animator>().Play("Desliz 0");
        }
    }
    public void SFXButton()
    {
        slider = GameObject.Find("SFXSlider");
        if(!sfx)
        {
            sfx = true;
            slider.GetComponent<Animator>().Play("Desliz");
        }
        else
        {
            sfx = false;
            slider.GetComponent<Animator>().Play("Desliz 0");
        }
    }

    //Tienda

    public void Potenciadores()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        TextAsset file = Resources.Load<TextAsset>("ModesDesc/"+buttonPressed+"Button");
        if(file != null)
        {
            descriptionPot.GetComponent<Text>().text = file.text;
        }
        buyButtonPot.SetActive(true);
        switch(buttonPressed)
        {
            case "Laser":
                itemName = "LASER";
                value = 1500;
                break;
            case "StopTime":
                itemName = "TIME STOP";
                value = 1500;
                break;
            case "Magnetic":
                itemName = "MAGNETICO";
                value = 1750;
                break;
            case "x2":
                itemName = "MULTIPLICADOR";
                value = 2000;
                break;
        }
    }
    public void Objetos()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        TextAsset file = Resources.Load<TextAsset>("DescObjetos/"+buttonPressed);
        if(file != null)
        {
            descriptionObj.GetComponent<Text>().text = file.text;
        }
        buyButtonObj.SetActive(true);
        switch(buttonPressed)
        {
            case "Vida":
                itemName = "1 VIDA EXTRA";
                value = 250;
                break;
            case "Inmune":
                itemName = "1 INMUNIDAD";
                value = 300;
                break;
            case "Cool":
                itemName = "1 RECARGA DE COOLDOWN";
                value = 300;
                break;
            case "Deposit":
                itemName = "1 DEPOSITO SECRETO";
                value = 250;
                break;
            case "Check+":
                itemName = "1 ENTREGA RAPIDA";
                value = 325;
                break;
            case "Slot":
                itemName = "BOLSILLO EXTRA";
                value = 600;
                break;
        }
    }
    public void DisplayObjetos()
    {
        objText[0].text = "Tienes " + SaveData.current.extraVida.ToString();
        objText[1].text = "Tienes " + SaveData.current.inmune.ToString();
        objText[2].text = "Tienes " + SaveData.current.cool.ToString();
        objText[3].text = "Tienes " + SaveData.current.deposit.ToString();
        objText[4].text = "Tienes " + SaveData.current.check.ToString();
    }
    public void BlockerCheckerObjs()
    {
        if(SaveData.current.extraVida == 5)
        {
            blockers[4].SetActive(true);
        }
        else
        {
            blockers[4].SetActive(false);
        }
        if(SaveData.current.inmune == 5)
        {
            blockers[5].SetActive(true);
        }
        else
        {
            blockers[5].SetActive(false);
        }
        if(SaveData.current.cool == 5)
        {
            blockers[6].SetActive(true);
        }
        else
        {
            blockers[6].SetActive(false);
        }
        if(SaveData.current.deposit == 5)
        {
            blockers[7].SetActive(true);
        }
        else
        {
            blockers[7].SetActive(false);
        }
        if(SaveData.current.check == 5)
        {
            blockers[8].SetActive(true);
        }
        else
        {
            blockers[8].SetActive(false);
        }
    }
    public void Cosmeticos()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        switch(buttonPressed)
        {
            case "Skin1":
                itemName = "SKIN INFERNO";
                value = 1250;
                break;
            case "Skin2":
                itemName = "SKIN RADIACTIVA";
                value = 1000;
                break;
            case "Skin3":
                itemName = "SKIN LIGHT";
                value = 1000;
                break;
            case "Skin4":
                itemName = "SKIN RETRO";
                value = 1250;
                break;
        }        
        CoinChecker();
    }
    public void ConfirmationScreen()
    {
        shopScreen.SetActive(false);
        confirmationScreen.SetActive(true);
        confirmationScreen.transform.GetChild(2).gameObject.SetActive(true);
        confirmationScreen.transform.GetChild(3).gameObject.SetActive(true);
        confirmationText.text = "¿Estas seguro que quieres comprar " + itemName + " por " + value + " monedas?";
    }
    public void CoinChecker()
    {
        buyButtonObj.SetActive(false);
        descriptionObj.text = "";
        buyButtonPot.SetActive(false);
        descriptionPot.text = "";
        confirmationScreen.transform.GetChild(2).gameObject.SetActive(false);
        confirmationScreen.transform.GetChild(3).gameObject.SetActive(false);
        confirmationScreen.transform.GetChild(4).gameObject.SetActive(false);
        if(SaveData.current.monedas > value)
        {
            ConfirmationScreen();
        }
        else
        {
            NoCoin();
        }
    }
    public void NoCoin()
    {
        shopScreen.SetActive(false);
        confirmationScreen.SetActive(true);
        confirmationScreen.transform.GetChild(4).gameObject.SetActive(true);
        confirmationText.text = "No tienes las monedas suficientes para comprar " + itemName;
    }
    public void CancelBuy()
    {
        shopScreen.SetActive(true);
        confirmationScreen.SetActive(false);
    }
    public void ConfirmBuy()
    {
        SaveData.current.monedas -= value;
        switch(buttonPressed)
        {
            #region Potenciadores
            case "Laser":
                SaveData.current.laser = true;
                blockers[0].SetActive(true);
                break;
            case "StopTime":
                SaveData.current.stopTime = true;
                blockers[1].SetActive(true);
                break;
            case "Magnetic":
                SaveData.current.magnetic = true;
                blockers[2].SetActive(true);
                break;
            case "x2":
                SaveData.current.x2 = true;
                blockers[3].SetActive(true);
                break;
            #endregion
            #region Objetos
            case "Vida":
                SaveData.current.extraVida += 1;
                break;
            case "Inmune":
                SaveData.current.inmune += 1;
                break;
            case "Cool":
                SaveData.current.cool += 1;
                break;
            case "Deposit":
                SaveData.current.deposit += 1;
                break;
            case "Check+":
                SaveData.current.check += 1;
                break;
            case "Slot":
                SaveData.current.slot = true;
                blockers[9].SetActive(true);
                break;
            #endregion
            #region Cosmeticos
            case "Skin1":
                SaveData.current.skinInferno = true;
                blockers[10].SetActive(true);
                break;
            case "Skin2":
                SaveData.current.skinRadiactive = true;
                blockers[11].SetActive(true);
                break;
            case "Skin3":
                SaveData.current.skinLight = true;
                blockers[12].SetActive(true);
                break;
            case "Skin4":
                SaveData.current.skinRetro = true;
                blockers[13].SetActive(true);
                break;
            #endregion
        }
        CancelBuy();
        OnSaveGame();
        OnLoadGame();
    }
    public void CoinPanel()
    {
        if(coinPanel.activeSelf == false)
        {
            coinPanel.SetActive(true);
            if(shopScreen.activeSelf)
            {
                shopScreen.SetActive(false);
                previousScreen = shopScreen;
            }
            if(Main.activeSelf)
            {
                Main.SetActive(false);
                previousScreen = Main;
            } 
        }
        else
        {
            coinPanel.SetActive(false);
            if(previousScreen == shopScreen)
            {
                shopScreen.SetActive(true);
            }
            if(previousScreen == Main)
            {
                Main.SetActive(true);
            }
        }
    }
    public void CoinBuy()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        switch(buttonPressed)
        {
            case "Bundle1":
                //Monetizar
                break;
            case "Bundle2":
                //Monetizar
                break;
            case "Bundle3":
                //Monetizar
                break;
            case "Bundle4":
                //Monetizar
                break;
            case "Bundle5":
                //Monetizar
                break;
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Main_Menu_Manager : MonoBehaviour
{
    #region Variables
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
    public Text titleObj;
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
    //Skin Selector
    public GameObject SkinSelectorScreen;
    public Text nameSkin;
    int selectedSkin;
    public GameObject isSelected;
    RectTransform isSelectedTransf;
    #endregion
    #region Callbacks
    void Start()
    {
        isSelectedTransf = isSelected.GetComponent<RectTransform>();
        OnLoadGame();   
        Time.timeScale = 1;
        selectMode();
        if(SaveData.current.actualSkin == "")
        {
            SaveData.current.actualSkin = "INICIAL";
        }
        nameSkin.text = SaveData.current.actualSkin;
        BlockerChekcerPot();
        BlockerCheckerObjs();
        BlockerCheckerCosmetics();
    }
    #endregion
    public void SumMonedas()
    {
        SaveData.current.monedas += 1000;
        OnSaveGame();
        OnLoadGame();
    }
    #region Displays
    void DisplayUniversalCoins()
    {
        foreach (var item in universalCoins)
        {
            item.text = SaveData.current.monedas.ToString();
        }
    }
    #endregion
    #region Guardado
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
    #endregion
    #region UIElements
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
        SceneManager.LoadScene("ModoEndless");
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
    #endregion
    #region Tienda
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
    public void BlockerChekcerPot()
    {
        if(SaveData.current.laser)
        {
            blockers[0].SetActive(true);
        }
        else
        {
            blockers[0].SetActive(false);
        }
        if(SaveData.current.stopTime)
        {
            blockers[1].SetActive(true);
        }
        else
        {
            blockers[1].SetActive(false);
        }
        if(SaveData.current.magnetic)
        {
            blockers[2].SetActive(true);
        }
        else
        {
            blockers[2].SetActive(false);
        }
        if(SaveData.current.x2)
        {
            blockers[3].SetActive(true);
        }
        else
        {
            blockers[3].SetActive(false);
        }
    }
    public void Objetos()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        switch(buttonPressed)
        {
            case "Vida":
                itemName = "VIDA EXTRA";
                value = 250;
                break;
            case "Inmune":
                itemName = "INMUNIDAD";
                value = 300;
                break;
            case "Cool":
                itemName = "RECARGA DE COOLDOWN";
                value = 300;
                break;
            case "Deposit":
                itemName = "DEPOSITO SECRETO";
                value = 250;
                break;
            case "Check+":
                itemName = "ENTREGA RAPIDA";
                value = 325;
                break;
            case "Slot":
                itemName = "BOLSILLO EXTRA";
                value = 600;
                break;
        }
        titleObj.text = itemName;
        TextAsset file = Resources.Load<TextAsset>("DescObjetos/"+buttonPressed);
        if(file != null)
        {
            descriptionObj.GetComponent<Text>().text = file.text;
        }
        buyButtonObj.SetActive(true);
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
        if(SaveData.current.slot)
        {
            blockers[9].SetActive(true);
        }
        else
        {
            blockers[9].SetActive(false);
        }
    }
    public void Cosmeticos()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        switch(buttonPressed)
        {
            case "blockerInferno":
                itemName = "SKIN INFERNO";
                value = 1250;
                break;
            case "blockerRadioactive":
                itemName = "SKIN RADIACTIVA";
                value = 1000;
                break;
            case "blockerLight":
                itemName = "SKIN LIGHT";
                value = 1000;
                break;
            case "blockerRetro":
                itemName = "SKIN RETRO";
                value = 1250;
                break;
        }        
        CoinChecker();
    }
    public void BlockerCheckerCosmetics()
    {
        if(SaveData.current.skinInferno)
        {
            blockers[10].SetActive(false);
        }
        else
        {
            blockers[10].SetActive(true);
        }
        if(SaveData.current.skinRadiactive)
        {
            blockers[11].SetActive(false);
        }
        else
        {
            blockers[11].SetActive(true);
        }
        if(SaveData.current.skinLight)
        {
            blockers[12].SetActive(false);
        }
        else
        {
            blockers[12].SetActive(true);
        }
        if(SaveData.current.skinRetro)
        {
            blockers[13].SetActive(false);
        }
        else
        {
            blockers[13].SetActive(true);
        }
    }
    public void ConfirmationScreen()
    {
        if(shopScreen.activeSelf)
        {
            shopScreen.SetActive(false);
            previousScreen = shopScreen;
        }
        else
        {
            SkinSelectorScreen.SetActive(false);
        }
        confirmationScreen.SetActive(true);
        confirmationScreen.transform.GetChild(2).gameObject.SetActive(true);
        confirmationScreen.transform.GetChild(3).gameObject.SetActive(true);
        confirmationText.text = "¿Estas seguro que quieres comprar " + itemName + " por " + value + " monedas?";
    }
    public void CoinChecker()
    {
        if(shopScreen.activeSelf)
        {
            buyButtonObj.SetActive(false);
            titleObj.text = "";
            descriptionObj.text = "";
            buyButtonPot.SetActive(false);
            descriptionPot.text = "";
        }
        confirmationScreen.transform.GetChild(2).gameObject.SetActive(false);
        confirmationScreen.transform.GetChild(3).gameObject.SetActive(false);
        confirmationScreen.transform.GetChild(4).gameObject.SetActive(false);
        if(SaveData.current.monedas >= value)
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
        if(shopScreen.activeSelf)
        {
            shopScreen.SetActive(false);
            previousScreen = shopScreen;
        }
        else
        {
            SkinSelectorScreen.SetActive(false);
        }
        confirmationScreen.SetActive(true);
        confirmationScreen.transform.GetChild(4).gameObject.SetActive(true);
        confirmationText.text = "No tienes las monedas suficientes para comprar " + itemName;
    }
    public void CancelBuy()
    {
        if(previousScreen == shopScreen)
        {
            shopScreen.SetActive(true);
        }
        else
        {
            SkinSelectorScreen.SetActive(true);
        }
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
            case "blockerInferno":
                SaveData.current.skinInferno = true;
                blockers[10].SetActive(true);
                break;
            case "blockerRadioactive":
                SaveData.current.skinRadiactive = true;
                blockers[11].SetActive(true);
                break;
            case "blockerLight":
                SaveData.current.skinLight = true;
                blockers[12].SetActive(true);
                break;
            case "blockerRetro":
                SaveData.current.skinRetro = true;
                blockers[13].SetActive(true);
                break;
            #endregion
        }
        CancelBuy();
        BlockerChekcerPot();
        BlockerCheckerObjs();
        BlockerCheckerCosmetics();
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
    public void SkinActivation()
    {
        if(SkinSelectorScreen.activeSelf == false)
        {
            Main.SetActive(false);
            SkinSelectorScreen.SetActive(true);
        }
        else
        {
            Main.SetActive(true);
            SkinSelectorScreen.SetActive(false);
        }
        SkinSelector();
    }
    public void SkinSelectorNavigator()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        if(buttonPressed == "RightArrow" && selectedSkin != 4)
        {
            selectedSkin++;
        }
        if(buttonPressed == "LeftArrow" && selectedSkin != 0)
        {
            selectedSkin--;
        }
        SkinSelector();
    }
    public void SkinSelector()
    {
        buttonPressed = EventSystem.current.currentSelectedGameObject.name;
        switch(buttonPressed)
        {
            case "skinInicial":
                nameSkin.text = "INICIAL";
                break;
            case "skinInferno":
                nameSkin.text = "INFERNO";
                break;
            case "skinRadioactive":
                nameSkin.text = "RADIACTIVO";
                break;
            case "skinLight":
                nameSkin.text = "LIGHT";
                break;
            case "skinRetro":
                nameSkin.text = "RETRO";
                break;
        }
        SkinSelected();
    }
    public void SkinSelected()
    {
        if(nameSkin.text != "")
        {
            SaveData.current.actualSkin = nameSkin.text;
        }
        switch(SaveData.current.actualSkin)
        {
            case "INICIAL":
                isSelectedTransf.anchoredPosition = new Vector2(27 ,isSelectedTransf.anchoredPosition.y);
                break;
            case "INFERNO":
                isSelectedTransf.anchoredPosition = new Vector2(-248 ,isSelectedTransf.anchoredPosition.y);
                break;
            case "RADIACTIVO":
                isSelectedTransf.anchoredPosition = new Vector2(-523 ,isSelectedTransf.anchoredPosition.y);
                break;
            case "LIGHT":
                isSelectedTransf.anchoredPosition = new Vector2(302 ,isSelectedTransf.anchoredPosition.y);
                break;
            case "RETRO":
                isSelectedTransf.anchoredPosition = new Vector2(577 ,isSelectedTransf.anchoredPosition.y);
                break;
        }
        OnSaveGame();
    }
    public void RewardedAd()
    {
        
    }
}
#endregion
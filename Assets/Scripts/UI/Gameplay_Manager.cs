using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Advertisements;
public class Gameplay_Manager : MonoBehaviour, IUnityAdsListener
{
    #region Variables    
    //UI
    //Main
    public GameObject MainButtons;
    public GameObject LifeCont;
    public GameObject previousLife;
    public GameObject tunnelEffect;
    GameObject modeButton1;
    GameObject modeButton2;
    GameObject objButton1;
    GameObject objButton2;
    GameObject objButton3;
    public bool Button1inCool = false;
    public bool Button2inCool = false;
    bool refreshCooldown;
    [Space (10)]
    //Panel
    public GameObject panel;
    public GameObject modesSelector;
    public GameObject objSelector;
    public GameObject confirmScreen;
    public string modeSelected1;
    public string modeSelected2;    
    public string objSelected1;
    public string objSelected2;
    public string objSelected3;
    public GameObject[] objToBlock;
    public GameObject selectedButton;
    string exitIdentificator;
    public GameObject startButton;
    public string _name;
    Color32 notBuyedColor = new Color32(46, 46, 46, 255);
    Color32 unactiveColor = new Color32(255, 255, 255, 255);
    Color32 lockedColor = new Color32(106, 106, 106, 255);
    [Space(10)]
    //Tuto
    public GameObject TutoPanel;
    GameObject TutoScreen;
    int TutoScreenIndex;
    public GameObject [] TutoScreens;
    [Space (10)]
    //Link moto
    public GameObject Moto;
    playerProprieties playerProprieties;
    playerMovement playerMovement;
    public static bool banderObjCheck = false;
    [Space (10)]
    //Displays
    public Text fpsDisplay;
    public Text timerText;
    public Text [] fuelTexts;
    public Slider fuelSlider;
    public Text coins;
    public Text [] safeCoinsTexts;
    public Text button1Text;
    public Text button2Text;
    public Text Pizzas;
    public Text descriptionPot;
    public Text descriptionObj;
    float minutes; //Display de minutos en el texto
    float seconds; //Display de segundos en el texto
    [Space (10)]
    //Audio
    GameObject AudioController;
    public AudioController audioController;
    //For Save Checkers
    public GameObject[] modeButtons;
    public GameObject[] objButtons;
    public Text[] objTexts;
    public Text[] objTextsUI;
    public int forRandomReward;
    public List<int> objList = new List<int>();
    //Ads
    public GameObject randomObjAd;
    string GooglePlayID = "4426982";
    bool testMode = true;
    string myPlacementId;
    #endregion
    #region Callbacks
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(GooglePlayID, testMode);
        banderObjCheck = false;
        OnLoadGame();
        AudioController = GameObject.Find("AudioController");
        if(AudioController != null)
        {
            audioController = AudioController.GetComponent<AudioController>();
            audioController.MusicChecker();
            audioController.LoadElements();  
        }
        ClosePauseScreen();
        Time.timeScale = 0;
        if(!SaveData.current.firstTimePassed)
        {
            SaveData.current.tutoActive = true;
            SaveData.current.firstTimePassed = true;
        }
        if(SaveData.current.tutoActive)
        {
            panel.SetActive(false);
            TutoPanel.SetActive(true);        
        }
        else
        {
            panel.SetActive(true);
            TutoPanel.SetActive(false);
        }
        TutoScreen = TutoPanel.transform.GetChild(0).gameObject;
        playerProprieties = Moto.GetComponent<playerProprieties>();
        playerMovement = Moto.GetComponent<playerMovement>();
        previousLife = LifeCont.transform.GetChild(playerProprieties.generalLife).gameObject;
        modeStatusChecker();
        objSelectorAmountChecker();
        objUIAmountChecker();
        ExtraLifeChecker();
        ExtraCheckChecker();
        RefreshCooldownChecker();
        DepositChecker();
        InmuneChecker();
        objAmountCheckerForAd();
        ShowBanner();
    }
    void Update()
    {
        FPSCounter();
        DisplayFuel();
        DisplayCoins();
        DisplayPizzas();
    }
    #endregion  
    #region Displays

    void FPSCounter()
    {
        Application.targetFrameRate = 30;
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = fps.ToString("f0");
    }
    void DisplayTime(float timeToDisplay) //Formato para el display del timer
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        minutes = Mathf.FloorToInt(playerProprieties.currentTime / 60);
        seconds = Mathf.FloorToInt(playerProprieties.currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void DisplayFuel()
    {
        foreach (var fuelText in fuelTexts)
        {
            fuelText.text = playerProprieties.currentFuel.ToString("f0");
        }
        fuelSlider.value = playerProprieties.currentFuel;
    }
    void DisplayCoins()
    {
        coins.text = playerProprieties.currentCoin.ToString();
        foreach (var safeCoinsText in safeCoinsTexts)
        {
            safeCoinsText.text = playerProprieties.generalCoin.ToString("f0");
        }
    }
    void DisplayPizzas()
    {
        Pizzas.text = playerProprieties.pizzas.ToString();
    }
    public void PrintDescriptionModes()
    {
        TextAsset file = Resources.Load<TextAsset>("ModesDesc/"+_name);
        if(file != null)
        {
            descriptionPot.GetComponent<Text>().text = file.text;
        }
    }
    public void PrintDescriptionObj()
    {
        TextAsset file = Resources.Load<TextAsset>("DescObjetos/"+_name);
        if(file != null)
        {
            descriptionObj.GetComponent<Text>().text = file.text;
        }
    }
    public void ShowTuto()
    {
        TutoScreenIndex = TutoScreenIndex + 1;
        foreach (var screen in TutoScreens)
        {
            screen.SetActive(false);
        }
        if(TutoScreenIndex == 1)
        {
            TutoScreens[0].SetActive(true);
        }
        if(TutoScreenIndex == 2)
        {
            TutoScreens[1].SetActive(true);
        }
        if(TutoScreenIndex == 3)
        {
            TutoScreens[2].SetActive(true);
        }
        if(TutoScreenIndex == 4)
        {
            TutoScreens[3].SetActive(true);
        }
        if(TutoScreenIndex == 5)
        {
            TutoScreens[4].SetActive(true);
        }
        if(TutoScreenIndex == 6)
        {
            TutoScreens[5].SetActive(true);
        }
        if(TutoScreenIndex == 7)
        {
            TutoScreens[6].SetActive(true);
        }
        if(TutoScreenIndex == 8)
        {
            TutoScreens[7].SetActive(true);
        }
        if(TutoScreenIndex == 9)
        {
            TutoScreens[8].SetActive(true);
        }
        if(TutoScreenIndex == 10)
        {
            TutoScreens[9].SetActive(true);
        }
        if(TutoScreenIndex == 11)
        {
            TutoScreenIndex = 1;
            TutoScreens[0].SetActive(true);
        }
    }
    #endregion
    #region UIelements
    public void objSelectorAmountChecker()
    {
        objTexts[0].text = SaveData.current.extraVida.ToString();
        objTexts[1].text = SaveData.current.inmune.ToString();
        objTexts[2].text = SaveData.current.cool.ToString();
        objTexts[3].text = SaveData.current.deposit.ToString();
        objTexts[4].text = SaveData.current.check.ToString();
        if(SaveData.current.extraVida == 0)
        {
            objButtons[0].GetComponent<Image>().color = notBuyedColor;
            objButtons[0].GetComponent<Button>().enabled = false;
        }
        else
        {
            objButtons[0].GetComponent<Image>().color = lockedColor;
            objButtons[0].GetComponent<Button>().enabled = true;
        }
        if(SaveData.current.inmune == 0)
        {
            objButtons[1].GetComponent<Image>().color = notBuyedColor;
            objButtons[1].GetComponent<Button>().enabled = false;
        }
        else
        {
            objButtons[1].GetComponent<Image>().color = lockedColor;
            objButtons[1].GetComponent<Button>().enabled = true;
        }
        if(SaveData.current.cool == 0)
        {
            objButtons[2].GetComponent<Image>().color = notBuyedColor;
            objButtons[2].GetComponent<Button>().enabled = false;
        }
        else
        {
            objButtons[2].GetComponent<Image>().color = lockedColor;
            objButtons[2].GetComponent<Button>().enabled = true;
        }
        if(SaveData.current.deposit == 0)
        {
            objButtons[3].GetComponent<Image>().color = notBuyedColor;
            objButtons[3].GetComponent<Button>().enabled = false;
        }
        else
        {
            objButtons[3].GetComponent<Image>().color = lockedColor;
            objButtons[3].GetComponent<Button>().enabled = true;
        }
        if(SaveData.current.check == 0)
        {
            objButtons[4].GetComponent<Image>().color = notBuyedColor;
            objButtons[4].GetComponent<Button>().enabled = false;
        }
        else
        {
            objButtons[4].GetComponent<Image>().color = lockedColor;
            objButtons[4].GetComponent<Button>().enabled = true;
        }
    }
    public void objUIAmountChecker()
    {
        objTextsUI[0].text = SaveData.current.extraVida.ToString();
        objTextsUI[1].text = SaveData.current.inmune.ToString();
        objTextsUI[2].text = SaveData.current.cool.ToString();
        objTextsUI[3].text = SaveData.current.deposit.ToString();
        objTextsUI[4].text = SaveData.current.check.ToString();
    }
    public void modeStatusChecker()
    {
        if(!SaveData.current.laser)
        {
            modeButtons[0].GetComponent<Image>().color = notBuyedColor;
            modeButtons[0].GetComponent<Button>().enabled = false;
        }
        else
        {
            modeButtons[0].GetComponent<Image>().color = lockedColor;
            modeButtons[0].GetComponent<Button>().enabled = true;
        }
        if(!SaveData.current.stopTime)
        {
            modeButtons[1].GetComponent<Image>().color = notBuyedColor;
            modeButtons[1].GetComponent<Button>().enabled = false;
        }
        else
        {
            modeButtons[1].GetComponent<Image>().color = lockedColor;
            modeButtons[1].GetComponent<Button>().enabled = true;
        }
        if(!SaveData.current.magnetic)
        {
            modeButtons[2].GetComponent<Image>().color = notBuyedColor;
            modeButtons[2].GetComponent<Button>().enabled = false;
        }
        else
        {
            modeButtons[2].GetComponent<Image>().color = lockedColor;
            modeButtons[2].GetComponent<Button>().enabled = true;
        }
        if(!SaveData.current.x2)
        {
            modeButtons[3].GetComponent<Image>().color = notBuyedColor;
            modeButtons[3].GetComponent<Button>().enabled = false;
        }
        else
        {
            modeButtons[3].GetComponent<Image>().color = lockedColor;
            modeButtons[3].GetComponent<Button>().enabled = true;
        }
    }
    public void OpenPauseScreen()
    {
        overUsedPanelon();
        panel.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosePauseScreen()
    {
        overUsedPaneloff();
        panel.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SaveData.current.monedas = SaveData.current.monedas + playerProprieties.generalCoin;
        OnSaveGame();
        OnLoadGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        overUsedPanelon();
        panel.transform.GetChild(1).gameObject.SetActive(true);
        Time.timeScale = 0;
        InterAd();
    }
    public void ResumeGameOver()
    {
        overUsedPaneloff();
        playerProprieties.generalCoin = playerProprieties.generalCoin - 50;
        playerProprieties.generalLife = 0;
        LifeChange();
        panel.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void SkipTutorial()
    {
        TutoPanel.SetActive(false);
        panel.SetActive(true);
        SaveData.current.tutoActive = false;
    }
    public void ChangeSelector()
    {
        if(objSelector.activeSelf)
        {
            objSelector.SetActive(false);
            modesSelector.SetActive(true);
        }
        else
        {
            objSelector.SetActive(true);
            modesSelector.SetActive(false);
        }
    }
    public void StartGame()
    {
        overUsedPaneloff();
        modesSelector.SetActive(false);
        modeButton1 = GameObject.Find(modeSelected1);
        modeButton1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        modeButton1.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        modeButton1.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        modeButton1.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 175);

        modeButton2 = GameObject.Find(modeSelected2);
        modeButton2.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
        modeButton2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
        modeButton2.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        modeButton2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 175);

        if(objSelected1 != "")
        {
            objButton1 = GameObject.Find(objSelected1);
            objButton1.GetComponent<RectTransform>().anchoredPosition = new Vector2(535, 200);
        }
        if(objSelected2 != "")
        {
            objButton2 = GameObject.Find(objSelected2);
            objButton2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-535, 200);
        }
        if(objSelected3 != "")
        {
            objButton3 = GameObject.Find(objSelected3);
            objButton3.GetComponent<RectTransform>().anchoredPosition = new Vector2(335, 200);
        }
        Time.timeScale = 1;
    }
    public void ObjSelector()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;
        if(objSelected1 == "" && selectedButton.name != objSelected2 && selectedButton.name != objSelected3)
        {
            objSelected1 = selectedButton.name;
            _name = objSelected1;
            PrintDescriptionObj();
            selectedButton.GetComponent<Image>().color = unactiveColor;
            return;
        }
        else if(objSelected1 == selectedButton.name)
        {
            objSelected1 = "";
            selectedButton.GetComponent<Image>().color = lockedColor;
            descriptionObj.GetComponent<Text>().text = "";
            return;
        }
        if(objSelected2 == "" && selectedButton.name != objSelected1 && selectedButton.name != objSelected3)
        {
            objSelected2 = selectedButton.name;
            _name = objSelected2;
            PrintDescriptionObj();
            selectedButton.GetComponent<Image>().color = unactiveColor;
            return;
        }
        else if(objSelected2 == selectedButton.name)
        {
            objSelected2 = "";
            selectedButton.GetComponent<Image>().color = lockedColor;
            descriptionObj.GetComponent<Text>().text = "";
            return;
        }
        if(SaveData.current.slot)
        {
            if(objSelected3 == "" && selectedButton.name != objSelected1 && selectedButton.name != objSelected2)
            {
                objSelected3 = selectedButton.name;
                _name = objSelected3;
                PrintDescriptionObj();
                selectedButton.GetComponent<Image>().color = unactiveColor;
                return;
            }
            else if(objSelected3 == selectedButton.name)
            {
                objSelected3 = "";
                selectedButton.GetComponent<Image>().color = lockedColor;
                descriptionObj.GetComponent<Text>().text = "";
                return;
            }
        } 
    }
    public void ModeSelector()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;
        if(modeSelected1 == "" && modeSelected2!=selectedButton.name)
        {
            modeSelected1 = selectedButton.name;
            _name = modeSelected1;
            PrintDescriptionModes();
            selectedButton.GetComponent<Image>().color = unactiveColor;
            if(modeSelected1 != "" && modeSelected2 != "")
            {
                startButton.GetComponent<Button>().enabled = true;
                startButton.GetComponent<Image>().color = unactiveColor;     
            }
            return;
        }
        if(modeSelected2 == "" && modeSelected1!=selectedButton.name)
        {
            modeSelected2 = selectedButton.name;
            _name = modeSelected2;
            PrintDescriptionModes();
            selectedButton.GetComponent<Image>().color = unactiveColor;
            if(modeSelected1 != "" && modeSelected2 != "")
            {
                startButton.GetComponent<Button>().enabled = true;
                startButton.GetComponent<Image>().color = unactiveColor;     
            }
            return;
        }
        if(selectedButton.name == modeSelected1)
        {
            modeSelected1 = "";
            startButton.GetComponent<Button>().enabled = false;
            startButton.GetComponent<Image>().color = lockedColor;
            selectedButton.GetComponent<Image>().color = lockedColor;
            descriptionPot.GetComponent<Text>().text = "";
        }
        if(selectedButton.name == modeSelected2)
        {
            modeSelected2 = "";
            startButton.GetComponent<Button>().enabled = false;
            startButton.GetComponent<Image>().color = lockedColor;
            selectedButton.GetComponent<Image>().color = lockedColor;
            descriptionPot.GetComponent<Text>().text = "";
        }
    }
    public void LifeChange()
    {   
        ExtraLifeChecker();
        LifeCont.transform.GetChild(playerProprieties.generalLife).gameObject.SetActive(true);
        if(previousLife != null)
        {
            previousLife.SetActive(false);
        }
        previousLife = LifeCont.transform.GetChild(playerProprieties.generalLife).gameObject;
        
        
    }
    public void ExitToMainMenu()
    {
        SaveData.current.monedas = SaveData.current.monedas + playerProprieties.generalCoin;
        OnSaveGame();
        OnLoadGame();
        SceneManager.LoadScene("Main_Menu");
    }
    public void GameOverScreenComprobation()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;
        if(playerProprieties.generalCoin > 25)
        {
            ConfirmationScreenActived();
        }
        else
        {
            if(selectedButton.name == "Reiniciar")
            {
                Restart();
            }
            else if(selectedButton.name == "Volver Menu")
            {
                ExitToMainMenu();
            }
        }
    }
    public void ConfirmationScreenActived()
    {
        panel.transform.GetChild(1).gameObject.SetActive(false);
        confirmScreen.SetActive(true);
        confirmScreen.transform.GetChild(1).GetComponent<Text>().text = "LAS MONEDAS ASEGURADAS SE GUARDARAN ¿QUIERES CONVERTIR " + playerProprieties.generalCoin + " EN " + playerProprieties.generalCoin*2 + " ?";
        if(selectedButton.name == "Reiniciar")
        {
            confirmScreen.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(Restart);
            exitIdentificator = selectedButton.name;
        }
        else if(selectedButton.name == "Volver Menu")
        {
            confirmScreen.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(ExitToMainMenu);
            exitIdentificator = selectedButton.name;
        }
    }
    public void Tutorial()
    {
        if(!TutoPanel.activeSelf)
        {
            TutoPanel.SetActive(true);
            ShowTuto();
            panel.SetActive(false);
        }
        else
        {
            TutoPanel.SetActive(false);
            panel.SetActive(true);
        }
    }
    
    #endregion
    #region Modos
    public void FastButton()
    {
        if(audioController != null)
        {
            audioController.musicMixer.SetFloat("MusicPitch", 1.5f);
        }
        tunnelEffect.GetComponent<Animator>().SetInteger("fastStatus", 1);
        if(modeButton1.name == "FastButton")
        {
            auxilarModule1();
            return;
        }
        else
        {
            auxilarModule2();
        }
    }
    public void TankButton()
    {
        if(audioController != null)
        {
            audioController.musicMixer.SetFloat("MusicPitch", 0.5f);
        }
        tunnelEffect.GetComponent<Animator>().SetInteger("tankStatus", 1);
        if(modeButton1.name == "TankButton")
        {
            auxilarModule1();
            return;
        }
        else
        {
            auxilarModule2();
        }
    }
    public void x2Button()
    {
        if(modeButton1.name == "x2Button")
        {
            auxilarModule1();
            return;
        }
        else
        {
            auxilarModule2();
        }
    }
    public void StopTimeButton()
    {
        if(audioController != null)
        {
            audioController.musicMixer.SetFloat("MusicPitch", 0.5f);
        }
        if(modeButton1.name == "StopTimeButton")
        {
            auxilarModule1();
            return;
        }
        else
        {
            auxilarModule2();
        }
    }
    public void MagneticButton()
    {
        if(modeButton1.name == "MagneticButton")
        {
            auxilarModule1();
            return;
        }
        else
        {
            auxilarModule2();
        }
    }
    public void LaserButton()
    {
        if(modeButton1.name == "LaserButton")
        {
            auxilarModule1();
            return;
        }
        else
        {
            auxilarModule2();
        }
    }
    #endregion
    #region Objetos
    public void RefreshCooldown()
    {
        refreshCooldown = true;
        SaveData.current.cool -= 1;
        objUIAmountChecker();
        RefreshCooldownChecker();
    }
    public void ExtraCheck()
    {
        //Bandera 
        banderObjCheck = true;
        SaveData.current.check -= 1;
        objUIAmountChecker();
        ExtraCheckChecker();
    }
    #endregion
    #region AuxilarMethods
    void auxilarModule1()
    {
        Button1inCool = true;
        RefreshCooldownChecker();
        if(!Button2inCool && (modeSelected1 != "x2Button" && modeSelected2 != "x2Button"))
        {
            modeButton2.GetComponent<Image>().color = lockedColor;
            modeButton2.GetComponent<Button>().enabled = false;
        }
        modeButton1.GetComponent<Button>().interactable = false;
        StartCoroutine(button1Cooldown());
    }
    void auxilarModule2()
    {
        Button2inCool = true;
        RefreshCooldownChecker();
        if(!Button1inCool && (modeSelected1 != "x2Button" && modeSelected2 != "x2Button"))
        {
            modeButton1.GetComponent<Image>().color = lockedColor;
            modeButton1.GetComponent<Button>().enabled = false;
        }
        modeButton2.GetComponent<Button>().interactable = false;
        StartCoroutine(button2Cooldown());
    }
    public void clearMode()
    {
        if(!Button1inCool)
        {
            modeButton1.GetComponent<Image>().color = unactiveColor;
            modeButton1.GetComponent<Button>().enabled = true;
        }
        if(!Button2inCool)
        {
            modeButton2.GetComponent<Image>().color = unactiveColor;
            modeButton2.GetComponent<Button>().enabled = true;
        }
        if(audioController != null)
        {
            audioController.musicMixer.SetFloat("MusicPitch", 1f);
        }
        tunnelEffect.GetComponent<Animator>().SetInteger("fastStatus", 0);
        tunnelEffect.GetComponent<Animator>().SetInteger("tankStatus", 0);
        
    }
    IEnumerator button1Cooldown(int secsTick = 1)
    {
        int secs = 30;
        while(secs > 0)
        {
            secs -= secsTick;
            button1Text.text = secs.ToString();
            if(refreshCooldown)
            {
                secs = 0;
                if(!Button2inCool)
                {
                    refreshCooldown = false;
                }
            }
            yield return new WaitForSeconds(secsTick);
        }
        button1Text.text = "";
        modeButton1.GetComponent<Button>().interactable = true;
        Button1inCool = false;
        RefreshCooldownChecker();
    }
    IEnumerator button2Cooldown(int secsTick = 1)
    {
        int secs = 30;
        while(secs > 0)
        {
            secs -= secsTick;
            button2Text.text = secs.ToString();
            if(refreshCooldown)
            {
                secs = 0;
                if(!Button1inCool)
                {
                    refreshCooldown = false;
                }
            }
            yield return new WaitForSeconds(secsTick);
        }
        button2Text.text = "";
        modeButton2.GetComponent<Button>().interactable = true;
        Button2inCool = false;
        RefreshCooldownChecker();
    }
    public void ExtraLifeChecker()
    {
        if(playerProprieties.generalLife != 2 && SaveData.current.extraVida != 0)
        {
            objToBlock[0].GetComponent<Button>().interactable = true;
            objToBlock[0].GetComponent<Image>().color = unactiveColor;
        }
        else
        {
            objToBlock[0].GetComponent<Button>().interactable = false;
            objToBlock[0].GetComponent<Image>().color = lockedColor;
        }
    }
    public void RefreshCooldownChecker()
    {
        if((Button1inCool || Button2inCool) && SaveData.current.cool != 0 && !playerProprieties.modeActive)
        {
            objToBlock[1].GetComponent<Button>().interactable = true;
            objToBlock[1].GetComponent<Image>().color = unactiveColor;
        }
        else
        {
            objToBlock[1].GetComponent<Button>().interactable = false;
            objToBlock[1].GetComponent<Image>().color = lockedColor;
        }
    }
    public void ExtraCheckChecker()
    {
        if(SaveData.current.check != 0)
        {
            objToBlock[2].GetComponent<Button>().interactable = true;
            objToBlock[2].GetComponent<Image>().color = unactiveColor;
        }
        else
        {
            objToBlock[2].GetComponent<Button>().interactable = false;
            objToBlock[2].GetComponent<Image>().color = lockedColor;
        }
    }
    public void DepositChecker()
    {
        if(SaveData.current.deposit != 0)
        {
            objToBlock[3].GetComponent<Button>().interactable = true;
            objToBlock[3].GetComponent<Image>().color = unactiveColor;
        }
        else
        {
            objToBlock[3].GetComponent<Button>().interactable = false;
            objToBlock[3].GetComponent<Image>().color = lockedColor;
        }
    }
    public void InmuneChecker()
    {
        if(!playerProprieties.inmune && SaveData.current.inmune != 0)
        {
            objToBlock[4].GetComponent<Button>().interactable = true;
            objToBlock[4].GetComponent<Image>().color = unactiveColor;
        }
        else
        {
            objToBlock[4].GetComponent<Button>().interactable = false;
            objToBlock[4].GetComponent<Image>().color = lockedColor;
        }
    }
    public void objAmountCheckerForAd()
    {
        if(SaveData.current.extraVida != 5)
        {
            objList.Add(1);
        }
        if(SaveData.current.deposit != 5)
        {
            objList.Add(2);
        }
        if(SaveData.current.inmune != 5)
        {
            objList.Add(3);
        }
        if(SaveData.current.check != 5)
        {
            objList.Add(4);
        }
        if(SaveData.current.cool != 5)
        {
            objList.Add(5);
        }
        if(objList.Count == 5)
        {
            randomObjAd.GetComponent<Image>().color = lockedColor;
            randomObjAd.GetComponent<Button>().interactable = false;
        }
    }
    //UI Auxiliares
    void overUsedPaneloff()
    {
        panel.SetActive(false);
        MainButtons.SetActive(true);
    }
    void overUsedPanelon()
    {
        panel.SetActive(true);
        MainButtons.SetActive(false);
    }
    //Save Auxiliares
    void OnLoadGame()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/player.save");
    }
    void OnSaveGame()
    {
        SerializationManager.Save(SaveData.current);
    }
    #endregion
    #region Ads
    public void InterAd()
    {
        if(Advertisement.IsReady("Interstitial_Android"))
        {
            Advertisement.Show("Interstitial_Android");
        }
    }
    public void RewardedAds()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;
        if(Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Defina lógica condicional para cada estado de finalización de anuncio:
        if (showResult == ShowResult.Finished && selectedButton != null)
        {
            switch(selectedButton.name)
            {
                case "RandomObj":
                    forRandomReward = Random.Range(0, objList.Count+1);
                    switch(forRandomReward)
                    {
                        case 0:
                            SaveData.current.extraVida +=1;
                            break;
                        case 1:
                            SaveData.current.deposit += 1;
                            break;
                        case 2:
                            SaveData.current.inmune += 1;
                            break;
                        case 3:
                            SaveData.current.check += 1;
                            break;
                        case 4:
                            SaveData.current.cool += 1;
                            break;
                    }
                    selectedButton.GetComponent<Image>().color = lockedColor;
                    selectedButton.GetComponent<Button>().interactable = false;
                    objSelectorAmountChecker();
                    OnSaveGame();
                    OnLoadGame();
                    break;
                case "ContinueGame":
                    overUsedPaneloff();
                    panel.transform.GetChild(1).gameObject.SetActive(false);
                    if(playerProprieties.currentFuel == 0)
                    {
                        playerProprieties.currentFuel = 150;
                    }
                    playerMovement.goingLeft = false;
                    playerMovement.goingRight = false;
                    selectedButton.GetComponent<Image>().color = lockedColor;
                    selectedButton.GetComponent<Button>().interactable = false;
                    Time.timeScale = 1;
                    break;
                case "Aceptar":
                    playerProprieties.generalCoin *= 2; 
                    if(exitIdentificator == "Restart")
                    {
                        Restart();
                    }
                    else
                    {
                        ExitToMainMenu();
                    }
                    break;
            }
            selectedButton = null;
            // Recompensa al usuario por ver el anuncio hasta su finalización.
            Debug.LogWarning("recompensa por ver anuncio.");
        }
        else if (showResult == ShowResult.Skipped)
        {
            switch(selectedButton.name)
            {
                case "Aceptar":
                    if(exitIdentificator == "Restart")
                    {
                        Restart();
                    }
                    else
                    {
                        ExitToMainMenu();
                    }
                    break;
            }
            // No recompensar al usuario por omitir el anuncio.
            Debug.LogWarning("Se omitio el anuncio.");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("El anuncio no finalizó debido a un error.");
        }
        }
        public void OnUnityAdsReady(string placementId)
        {
            // Si esta listo, muestre el anuncio:
            if (placementId == myPlacementId)
            {

            }
        }
        public void OnUnityAdsDidError(string message)
        {
            // errores aca
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            // Acciones opcionales a realizar cuando los usuarios finales activan un anuncio.
        }
        public void ShowBanner()
        {
            if(Advertisement.IsReady("Banner_Android"))
            {
                Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
                Advertisement.Show("Banner_Android");
                Debug.Log("banner listo");
            }
            else
            {
                StartCoroutine(RepeatShowBanner());
            }
        }
        public void HideBanner(){}
        IEnumerator RepeatShowBanner()
        {
            yield return new WaitForSeconds(1);
            ShowBanner();
        }
    #endregion
}
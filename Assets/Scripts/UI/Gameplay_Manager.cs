using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Gameplay_Manager : MonoBehaviour
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
    public bool Button1inCool = false;
    public bool Button2inCool = false;
    [Space (10)]
    //Panel
    public GameObject panel;
    public GameObject GOresumeButton;
    public GameObject modesSelector;
    public string modeSelected1;
    public string modeSelected2;    
    GameObject selectedButton;
    public GameObject startButton;
    public GameObject description;
    public string _name;
    Color32 activeColor = new Color32(148, 214, 255, 255);
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
    float minutes; //Display de minutos en el texto
    float seconds; //Display de segundos en el texto
    [Space (10)]
    //Audio
    GameObject AudioController;
    public AudioController audioController;
    //Save
    GameObject SaveScript;
    globalVariables saveScript;
    #endregion
    #region Callbacks
    void Start()
    {        
        AudioController = GameObject.Find("AudioController");
        if(AudioController != null)
        {
            audioController = AudioController.GetComponent<AudioController>();
            audioController.MusicChecker();
            audioController.LoadElements();  
        }
        ClosePauseScreen();
        Time.timeScale = 0;
        SaveScript = GameObject.Find("SaveManager");
        if(SaveScript != null)
        {
            saveScript = SaveScript.GetComponent<globalVariables>();
            if(!saveScript.firstTimePassed)
            {
                saveScript.tutoActive = true;
                saveScript.firstTimePassed = true;
            }
            if(saveScript.tutoActive)
            {
                panel.SetActive(false);
                TutoPanel.SetActive(true);
            }
            else
            {
                panel.SetActive(true);
                TutoPanel.SetActive(false);
            }
        }
        else
        {
            panel.SetActive(true);
        }
        TutoScreen = TutoPanel.transform.GetChild(0).gameObject;
        playerProprieties = Moto.GetComponent<playerProprieties>();
        previousLife = LifeCont.transform.GetChild(playerProprieties.generalLife).gameObject;
    }
    void Update()
    {
        FPSCounter();
        DisplayFuel();
        DisplayCoins();
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
        coins.text = playerProprieties.currentPizzas.ToString();
        foreach (var safeCoinsText in safeCoinsTexts)
        {
            safeCoinsText.text = playerProprieties.generalPizzas.ToString("f0");
        }
    }
    public void PrintDescription()
    {
        TextAsset file = Resources.Load<TextAsset>("ModesDesc/"+_name);
        if(file != null)
        {
            description.GetComponent<Text>().text = file.text;
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
            TutoScreens[1].SetActive(true);
        }
        if(TutoScreenIndex == 2)
        {
            TutoScreens[2].SetActive(true);
        }
        if(TutoScreenIndex == 3)
        {
            TutoScreens[3].SetActive(true);
        }
        if(TutoScreenIndex == 4)
        {
            TutoScreens[4].SetActive(true);
        }
        if(TutoScreenIndex == 5)
        {
            TutoScreens[5].SetActive(true);
        }
        if(TutoScreenIndex == 6)
        {
            TutoScreens[6].SetActive(true);
        }
        if(TutoScreenIndex == 7)
        {
            TutoScreens[7].SetActive(true);
        }
        if(TutoScreenIndex == 8)
        {
            TutoScreens[8].SetActive(true);
        }
        if(TutoScreenIndex == 9)
        {
            TutoScreens[9].SetActive(true);
        }
        if(TutoScreenIndex == 10)
        {
            TutoScreenIndex = 1;
            TutoScreens[1].SetActive(true);
        }
    }
    #endregion
    #region UIelements
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
        if(saveScript != null)
        {
            saveScript.monedas = saveScript.monedas + playerProprieties.currentPizzas;
            saveScript.SavePlayer();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        overUsedPanelon();
        panel.transform.GetChild(1).gameObject.SetActive(true);
        Time.timeScale = 0;
        if(playerProprieties.generalPizzas <= 50)
        {
            GOresumeButton.GetComponent<Button>().enabled = false;
            GOresumeButton.GetComponent<Image>().color = lockedColor;
        }
    }
    public void ResumeGameOver()
    {
        overUsedPaneloff();
        playerProprieties.generalPizzas = playerProprieties.generalPizzas - 50;
        playerProprieties.generalLife = 0;
        LifeChange();
        panel.transform.GetChild(1).gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void SkipTutorial()
    {
        TutoPanel.SetActive(false);
        panel.SetActive(true);
        modesSelector.SetActive(true);
        if(saveScript != null)
        {
            saveScript.tutoActive = false;
            saveScript.SavePlayer();
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
        if(saveScript != null)
        {
            if(saveScript.tutoActive)
            {
                TutoPanel.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void ModeSelector()
    {
        selectedButton = EventSystem.current.currentSelectedGameObject;
        if(modeSelected1 == "")
        {
            modeSelected1 = selectedButton.name;
            _name = modeSelected1;
            PrintDescription();
            selectedButton.GetComponent<Image>().color = unactiveColor;
            return;
        }
        if(modeSelected2 == "" && modeSelected1!=selectedButton.name)
        {
            modeSelected2 = selectedButton.name;
            _name = modeSelected2;
            PrintDescription();
            selectedButton.GetComponent<Image>().color = unactiveColor;
            if(modeSelected1 != "" && modeSelected2 != "")
            {
                startButton.GetComponent<Button>().enabled = true;
                startButton.GetComponent<Image>().color = unactiveColor;     
            }
            else
            {
                startButton.GetComponent<Button>().enabled = false;
                startButton.GetComponent<Image>().color = lockedColor;
            }
            return;
        }
        if(selectedButton.name == modeSelected1)
        {
            modeSelected1 = "";
            selectedButton.GetComponent<Image>().color = lockedColor;
            description.GetComponent<Text>().text = "";
        }
        if(selectedButton.name == modeSelected2)
        {
            modeSelected2 = "";
            selectedButton.GetComponent<Image>().color = lockedColor;
            description.GetComponent<Text>().text = "";
        }
    }
    public void LifeChange()
    {   
        LifeCont.transform.GetChild(playerProprieties.generalLife).gameObject.SetActive(true);
        previousLife.SetActive(false);
        previousLife = LifeCont.transform.GetChild(playerProprieties.generalLife).gameObject;
    }
    public void ExitToMainMenu()
    {
        if(saveScript != null)
        {
            saveScript.monedas = saveScript.monedas + playerProprieties.currentPizzas;
            saveScript.SavePlayer();
        }
        SceneManager.LoadScene("Main_Menu");
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
    #region AuxilarMethods
    void auxilarModule1()
    {
        Button1inCool = true;
        if(!Button2inCool && !playerProprieties.x2Mode)
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
        if(!Button1inCool && !playerProprieties.x2Mode)
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
            yield return new WaitForSeconds(secsTick);
        }
        button1Text.text = "";
        modeButton1.GetComponent<Button>().interactable = true;
        Button1inCool = false;
    }
    IEnumerator button2Cooldown(int secsTick = 1)
    {
        int secs = 30;
        while(secs > 0)
        {
            secs -= secsTick;
            button2Text.text = secs.ToString();
            yield return new WaitForSeconds(secsTick);
        }
        button2Text.text = "";
        modeButton2.GetComponent<Button>().interactable = true;
        Button2inCool = false;
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
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay_Manager : MonoBehaviour
{
    //UI
    public GameObject panel;
    public GameObject LifeCont;
    public GameObject SButton;
    public GameObject FButton;
    public GameObject TButton;
    public GameObject tunnelEffect;
    Color32 activeColor = new Color32(148, 214, 255, 255);
    Color32 unactiveColor = new Color32(255, 255, 255, 255);
    Color32 lockedColor = new Color32(106, 106, 106, 255);
    //Link moto
    public GameObject Moto;
    playerProprieties playerProprieties;
    //Displays
    public Text fpsDisplay;
    public Text modeText;
    public Text timerText;
    public Text fuelText;
    float minutes; //Display de minutos en el texto
    float seconds; //Display de segundos en el texto
    //Audio
    GameObject AudioController;
    AudioController audioController;
    
    void Start()
    {
        Time.timeScale = 1;
        AudioController = GameObject.Find("AudioController");
        if(AudioController != null)
        {
            audioController = AudioController.GetComponent<AudioController>();
            audioController.MusicChecker();
            audioController.LoadElements();
        }
        ClosePauseScreen();
        playerProprieties = Moto.GetComponent<playerProprieties>();

    }
    void Update()
    {
        FPSCounter();
        DisplayMode();
        DisplayTime(playerProprieties.currentTime);
        DisplayFuel();
    }
    void FPSCounter()
    {
        Application.targetFrameRate = 30;
        float fps = 1 / Time.unscaledDeltaTime;
        fpsDisplay.text = fps.ToString("f0");
    }
    void DisplayMode()
    {
        modeText.text = playerProprieties.vehicleType.ToString();
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
        fuelText.text = playerProprieties.currentFuel.ToString("f0");
    }
    public void OpenPauseScreen()
    {
        panel.SetActive(true);
        panel.transform.GetChild(0).gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosePauseScreen()
    {
        panel.SetActive(false);
        panel.transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        panel.SetActive(true);
        panel.transform.GetChild(1).gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    public void LifeDiscount()
    {
        LifeCont.transform.GetChild(playerProprieties.generalLife).gameObject.GetComponent<Image>().color = lockedColor;
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    public void vehicleButtonStandard()
    {
        if(audioController != null)
        {
            audioController.musicMixer.SetFloat("MusicPitch", 1f);
        }
        SButton.GetComponent<Image>().color = activeColor;
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
        if(audioController != null)
        {
            audioController.musicMixer.SetFloat("MusicPitch", 1.25f);
        }
        FButton.GetComponent<Image>().color = activeColor;
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
        if(audioController != null)
        {
            audioController.musicMixer.SetFloat("MusicPitch", 0.75f);
        }
        TButton.GetComponent<Image>().color = activeColor;
        tunnelEffect.GetComponent<Animator>().Play("TankEffect");
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

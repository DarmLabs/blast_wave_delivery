using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Main_Menu_Manager : MonoBehaviour
{
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
    void Start()
    {
        Time.timeScale = 1;
        selectMode();
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
        if(buttonPressed == "Moto")
        {
            motoSection.transform.SetSiblingIndex(2);
            pjSection.transform.SetSiblingIndex(1);
            musicSection.transform.SetSiblingIndex(0);
        }
        if(buttonPressed == "Pj")
        {
            pjSection.transform.SetSiblingIndex(2);
        }
        if(buttonPressed == "Musica")
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
}

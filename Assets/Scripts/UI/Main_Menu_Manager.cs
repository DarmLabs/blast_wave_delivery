using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Main_Menu_Manager : MonoBehaviour
{
    public GameObject shopScreen;
    public GameObject modeScreen;
    string modeName;
    public GameObject Main;
    GameObject slider;
    bool music;
    bool sfx;
    
    public void PlayButton()
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
    public void LoadMode()
    {
        modeName = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(modeName);
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

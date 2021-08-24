using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Main;
    GameObject slider;
    bool music;
    bool sfx;
    public void PlayButton()
    {
        SceneManager.LoadScene("Carretera");
    }
    public void ShopButton()
    {
        if(Panel.activeSelf == false)
        {
            Panel.SetActive(true);
            Main.SetActive(false);
        }
        else
        {
            Panel.SetActive(false);
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

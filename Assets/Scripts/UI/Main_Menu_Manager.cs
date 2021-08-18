using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Botones;
    public GameObject slider;
    public void PlayButton()
    {
        SceneManager.LoadScene("Carretera");
    }
    public void ShopButton()
    {
        Debug.Log("a");
        if(Panel.activeSelf == false)
        {
            Panel.SetActive(true);
            Botones.SetActive(false);
        }
        else
        {
            Panel.SetActive(false);
            Botones.SetActive(true);
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    void OnMouseEnter(GameObject go)
    {
        if(go.gameObject.name == "SFX" || go.gameObject.name == "SFXSlider")
        {
            slider = GameObject.Find("SFXSlider");
            slider.GetComponent<Animator>().Play("Desliz");
        }
    }
}

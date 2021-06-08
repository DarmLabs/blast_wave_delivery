using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("Carretera");
    }
}

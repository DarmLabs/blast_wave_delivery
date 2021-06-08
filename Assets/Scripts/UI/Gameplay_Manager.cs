using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay_Manager : MonoBehaviour
{
    public GameObject pauseScreen;
    void Start()
    {
        ClosePauseScreen();
    }
    public void PauseScreen()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    public void ClosePauseScreen()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}

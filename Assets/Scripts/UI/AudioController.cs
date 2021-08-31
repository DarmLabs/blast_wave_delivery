using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public GameObject musicSlider;
    public AudioMixer musicMixer;
    float musicValue = 1;
    AudioSource [] allMusicSources;
    public GameObject sfxSlider;
    public AudioMixer sfxMixer;
    float sfxValue = 1;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        allMusicSources = transform.Find("Music").GetComponentsInChildren<AudioSource>();
        MusicChecker();
        LoadElements();
    }
    public void LoadElements()
    {
        musicSlider = GameObject.Find("MusicSlider");
        musicSlider.GetComponent<Slider>().onValueChanged.AddListener(SetVolMusic);
        musicSlider.GetComponent<Slider>().value = musicValue;
        sfxSlider = GameObject.Find("SFXSlider");
        sfxSlider.GetComponent<Slider>().onValueChanged.AddListener(SetVolSFX);
        sfxSlider.GetComponent<Slider>().value = sfxValue;  
    }
    public void SetVolMusic(float musicSliderValue)
    {
        musicValue = musicSliderValue;
        musicMixer.SetFloat("MusicVol", Mathf.Log10(musicSliderValue) * 20);
    }
    public void SetVolSFX(float SFXSliderValue)
    {
        sfxValue = SFXSliderValue;
        sfxMixer.SetFloat("SFXVol", Mathf.Log10(SFXSliderValue) * 20);
    }
    public void MusicChecker()
    {
        foreach (var source in allMusicSources)
        {
            source.GetComponent<AudioSource>().Stop();
        }
        if(SceneManager.GetActiveScene().name == "Main_Menu")
        {
            transform.Find("Music/MusicMenu1").GetComponent<AudioSource>().Play();
        }
        if(SceneManager.GetActiveScene().name == "ModoEndless")
        {
            transform.Find("Music/MusicGameplay1").GetComponent<AudioSource>().Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class visualizer : MonoBehaviour
{
    public float minWidth = 30f;
    public float maxWidth = 300f;
    public float sensitivity = 0.25f;
    objectVisualizer [] objectVisualizers;
    public float [] audioSpectrum;
    public float spectrumValue;
    void Start()
    {
        audioSpectrum = new float [128];
        objectVisualizers = GetComponentsInChildren<objectVisualizer>();
    }
    void Update()
    {
        visualizerMethod();
    }
    public void visualizerMethod()
    {
        AudioListener.GetSpectrumData(audioSpectrum, 0, FFTWindow.Rectangular);
        if(audioSpectrum != null && audioSpectrum.Length > 0)
        {
            spectrumValue = audioSpectrum[0];
        }
        for (int i = 0; i < objectVisualizers.Length; i++)
        {
            Vector2 newSize = objectVisualizers[i].GetComponent<RectTransform>().rect.size;

            newSize.y = Mathf.Lerp(newSize.x, minWidth + (audioSpectrum[i] * (maxWidth - minWidth)), sensitivity);
            objectVisualizers [i].GetComponent<RectTransform> ().sizeDelta = newSize;
        }
    }
}

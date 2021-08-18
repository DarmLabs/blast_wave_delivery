using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject slider;
    void Start()
    {
        slider = GameObject.Find((gameObject.name)+"Slider");
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        slider.GetComponent<Animator>().Play("Desliz");
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        slider.GetComponent<Animator>().Play("Desliz 0");
    }
}

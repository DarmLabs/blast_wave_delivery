using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class showDesc : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject UI_Manager;
    Gameplay_Manager gameplay_Manager;
    void Start()
    {
        gameplay_Manager = UI_Manager.GetComponent<Gameplay_Manager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameplay_Manager._name = eventData.pointerEnter.gameObject.name;
        gameplay_Manager.PrintDescriptionModes();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        gameplay_Manager.descriptionPot.GetComponent<Text>().text = "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InputButtons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("ParentMoto");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(gameObject.name == "Right")
        {
            player.GetComponent<playerMovement>().goingRight = true;
        }
        if(gameObject.name == "Left")
        {
            player.GetComponent<playerMovement>().goingLeft = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(gameObject.name == "Right")
        {
            player.GetComponent<playerMovement>().goingRight = false;
        }
        if(gameObject.name == "Left")
        {
            player.GetComponent<playerMovement>().goingLeft = false;
        }
    }
}

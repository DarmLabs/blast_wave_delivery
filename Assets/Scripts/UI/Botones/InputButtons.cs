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
        if(gameObject.name == "RightP")
        {
            player.GetComponent<playerMovement>().goingRight = true;
        }
        if(gameObject.name == "LeftP")
        {
            player.GetComponent<playerMovement>().goingLeft = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(gameObject.name == "RightP")
        {
            player.GetComponent<playerMovement>().goingRight = false;
        }
        if(gameObject.name == "LeftP")
        {
            player.GetComponent<playerMovement>().goingLeft = false;
        }
    }
}

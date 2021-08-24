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
            player.GetComponent<playerMovementHorizontal>().goingRight = true;
        }
        if(gameObject.name == "Left")
        {
            player.GetComponent<playerMovementHorizontal>().goingLeft = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(gameObject.name == "Right")
        {
            player.GetComponent<playerMovementHorizontal>().goingRight = false;
        }
        if(gameObject.name == "Left")
        {
            player.GetComponent<playerMovementHorizontal>().goingLeft = false;
        }
    }
}

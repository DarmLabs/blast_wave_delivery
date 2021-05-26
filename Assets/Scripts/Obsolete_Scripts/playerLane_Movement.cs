using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLane_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public int laneChange = 3;
    Vector3 pos;
    bool isMoving = false;
    void Update()
    {
        pos = transform.position;
        if(Input.GetKeyDown("a") && laneChange != 1 && laneChange !=6 && laneChange != 11 && !isMoving)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(-6, 0, 0);
            StartCoroutine (stopSlide());
            laneChange = laneChange - 1;
            isMoving = true;
        }
        if(Input.GetKeyDown("d") && laneChange != 5 && laneChange !=10 && laneChange != 15 && !isMoving)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(6, 0, 0);
            StartCoroutine (stopSlide());
            laneChange = laneChange + 1;
            isMoving = true;
        }
        if(Input.GetKeyDown("s") && laneChange != 1 && laneChange !=2 && laneChange != 3 && laneChange != 4 && laneChange != 5 && !isMoving)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, -6, 0);
            StartCoroutine (stopSlide());
            laneChange = laneChange - 5;
            isMoving = true;
        }
        if(Input.GetKeyDown("w") && laneChange != 11 && laneChange !=12 && laneChange != 13 && laneChange != 14 && laneChange != 15 && !isMoving)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 6, 0);
            StartCoroutine (stopSlide());
            laneChange = laneChange + 5;
            isMoving = true;
        }

    }
    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds (.50f);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if(laneChange == 1)
        {
            transform.position = new Vector3(-6, 2, pos.z);
        }
        if(laneChange == 2)
        {
            transform.position = new Vector3(-3, 2, pos.z);
        }
        if(laneChange == 3)
        {
            transform.position = new Vector3(0, 2, pos.z);
        }
        if(laneChange == 4)
        {
            transform.position = new Vector3(3, 2, pos.z);
        }
        if(laneChange == 5)
        {
            transform.position = new Vector3(6, 2, pos.z);
        }
        if(laneChange == 6)
        {
            transform.position = new Vector3(-6, 5, pos.z);
        }
        if(laneChange == 7)
        {
            transform.position = new Vector3(-3, 5, pos.z);
        }
        if(laneChange == 8)
        {
            transform.position = new Vector3(0, 5, pos.z);
        }
        if(laneChange == 9)
        {
            transform.position = new Vector3(3, 5, pos.z);
        }
        if(laneChange == 10)
        {
            transform.position = new Vector3(6, 5, pos.z);
        }
        if(laneChange == 11)
        {
            transform.position = new Vector3(-6, 8, pos.z);
        }
        if(laneChange == 12)
        {
            transform.position = new Vector3(-3, 8, pos.z);
        }
        if(laneChange == 13)
        {
            transform.position = new Vector3(0, 8, pos.z);
        }
        if(laneChange == 14)
        {
            transform.position = new Vector3(3, 8, pos.z);
        }
        if(laneChange == 15)
        {
            transform.position = new Vector3(6, 8, pos.z);
        }
        isMoving = false;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform Player;
    public Vector3 offset;
    void FixedUpdate()
    {
       transform.position = Player.position + offset; 
    }
}

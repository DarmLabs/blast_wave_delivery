using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectLookAt : MonoBehaviour
{
    public FixedJoystick fixedJoystick;
    public Vector3 offset;
    public GameObject PlayerPosition;
    void FixedUpdate()
    {
        Vector3 direction = Vector3.up * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        transform.position = direction * 3 + offset + PlayerPosition.transform.position;
    }
}

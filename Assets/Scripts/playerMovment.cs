using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovment : MonoBehaviour
{
    public float speed;
    public FixedJoystick fixedJoystick;
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.up * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        transform.Translate(direction * speed * Time.fixedDeltaTime);
    }
}

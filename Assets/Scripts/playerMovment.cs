using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovment : MonoBehaviour
{
    public float xyspeed;

    public float zspeed;
    public FixedJoystick fixedJoystick;
    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.up * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        rb.AddForce(direction * xyspeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        transform.Translate(0, 0, zspeed);
       
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Piso")
        Debug.Log("Colision detectada");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementHorizontal : MonoBehaviour
{
    public Rigidbody rb;
    float speedx = 0.1f;
    float jump = 10;
    float speedz = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Inputs();
        transform.Translate(0,0, speedz);
    }

    void Inputs()
    {
        if(Input.GetKey("a"))
        {
            transform.Translate(-speedx,0,0);
        }
        if(Input.GetKey("d"))
        {
            transform.Translate(speedx,0,0);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jump, 0, ForceMode.Impulse);
        }
    }
}

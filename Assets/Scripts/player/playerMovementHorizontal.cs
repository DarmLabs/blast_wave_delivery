using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerMovementHorizontal : MonoBehaviour
{
    public Rigidbody rb;
    public float speedx = 60;
    public bool goingRight = false;
    public bool goingLeft = false;
    public float speedz = 40;
    Animator MotoAnimator;
    public float speedMultiplier;
    public float HMultiplier;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MotoAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        transform.Translate(0,0, speedz * (Time.deltaTime * speedMultiplier));
        if(goingRight)
        {
            InputRight();
            MotoAnimator.SetBool("goingRight",true);
        }
        if(!goingRight)
        {
            MotoAnimator.SetBool("goingRight",false);
        }
        if(goingLeft)
        {
            InputLeft();
            MotoAnimator.SetBool("goingLeft",true);
        }
        if(!goingLeft)
        {
            MotoAnimator.SetBool("goingLeft",false);
        }
    }

    void InputRight()
    {
        rb.AddForce(speedx * Time.deltaTime * HMultiplier,0,0,ForceMode.VelocityChange);
    }
    void InputLeft()
    {
        rb.AddForce(-speedx * Time.deltaTime * HMultiplier,0,0,ForceMode.VelocityChange);
    }
}

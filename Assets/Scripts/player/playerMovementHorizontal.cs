using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovementHorizontal : MonoBehaviour
{
    public Rigidbody rb;
    public float speedx = 60;
    float jump = 20;
    public float speedz = 40;
    public FixedJoystick fixedJoystick;
    bool isGrounded = true;
    Animator MotoAnimator;
    public float speedMultiplier;
    public float HMultiplier;
    public GameObject JoystickHandle;
    Animator JoystickAnimator;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        MotoAnimator = GetComponentInChildren<Animator>();
        JoystickAnimator = JoystickHandle.GetComponent<Animator>();
    }

    void Update()
    {
        Inputs();
        Vector3 direction = Vector3.right * fixedJoystick.Horizontal;
        rb.AddForce(direction.x * speedx * Time.deltaTime * HMultiplier,0,0,ForceMode.VelocityChange);
        transform.Translate(0,0, speedz * (Time.deltaTime * speedMultiplier));
        if(direction.x > 0.8)
        {
            MotoAnimator.SetBool("RightRotation", true);
            JoystickAnimator.SetBool("TurnRight", true);
        }
        else
        {
            MotoAnimator.SetBool("RightRotation", false);
            JoystickAnimator.SetBool("TurnRight", false);
        }
        if(direction.x < -0.8)
        {
            MotoAnimator.SetBool("LeftRotation", true);
            JoystickAnimator.SetBool("TurnLeft", true);
        }
        else
        {
            MotoAnimator.SetBool("LeftRotation", false);
            JoystickAnimator.SetBool("TurnLeft", false);
        }
        if(transform.position.y < 9.3)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Inputs()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jump, 0, ForceMode.Impulse);
        }
    }
}

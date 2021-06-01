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
    public Animator animator;
    public float speedMultiplier;
    public float HMultiplier;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Inputs();
        Vector3 direction = Vector3.right * fixedJoystick.Horizontal;
        rb.AddForce(direction.x * speedx * Time.deltaTime * HMultiplier,0,0,ForceMode.VelocityChange);
        transform.Translate(0,0, speedz * (Time.deltaTime * speedMultiplier));
        if(direction.x > 0.2)
        {
            animator.SetBool("RightRotation", true);
        }
        else
        {
            animator.SetBool("RightRotation", false);
        }
        if(direction.x < -0.2)
        {
            animator.SetBool("LeftRotation", true);
        }
        else
        {
            animator.SetBool("LeftRotation", false);
        }
        if(transform.position.y < 8.6)
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

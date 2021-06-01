using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovementHorizontal : MonoBehaviour
{
    public Rigidbody rb;
    float speedx = 40;
    float jump = 20;
    public float speedz = 40;
    public FixedJoystick fixedJoystick;
    bool isGrounded = true;
    public Animator animator;
    public float speedMultiplier;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Inputs();
        Vector3 direction = Vector3.right * fixedJoystick.Horizontal;
        rb.AddForce(direction.x * speedx * Time.deltaTime,0,0,ForceMode.VelocityChange);
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
    }

    void Inputs()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(0, jump, 0, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Piso")
        {
            isGrounded = true;
        }
    }
}

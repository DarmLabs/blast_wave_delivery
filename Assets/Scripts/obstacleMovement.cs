using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleMovement : MonoBehaviour
{
    new Rigidbody rigidbody;
    public float speed = 5f;
    void Start(){rigidbody = GetComponent<Rigidbody>();}
    void Update(){rigidbody.velocity = new Vector3 (0, 0, -speed);}
}

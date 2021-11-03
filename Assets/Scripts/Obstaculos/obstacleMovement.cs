using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleMovement : MonoBehaviour
{
    new Rigidbody rigidbody;
    public float speed;
    public float velocidadEscaladoObs2 = 2;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        rigidbody.velocity = new Vector3 (0, 0, -(speed+(velocidadEscaladoObs2*TileManager.escalado)));
    }
}

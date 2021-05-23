using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRotating : MonoBehaviour
{
    public Transform objLookAt;
    void FixedUpdate()
    {
        transform.LookAt(objLookAt);
    }
}

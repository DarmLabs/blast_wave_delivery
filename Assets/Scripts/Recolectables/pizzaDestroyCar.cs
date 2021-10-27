using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizzaDestroyCar : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "obs")
        {
            Destroy(other.gameObject);
        }
    }
}

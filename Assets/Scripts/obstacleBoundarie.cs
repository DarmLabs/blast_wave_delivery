using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleBoundarie : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    Destroy(other.gameObject);
  }
}

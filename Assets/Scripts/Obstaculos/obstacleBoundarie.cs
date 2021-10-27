using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleBoundarie : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    Destroy(other.gameObject);
    if (other.gameObject.tag == "Caja")
    {
        spawnManager.CajaExistente = false;
        spawnManager.CajaRecolectada = false;
        spawnManager.ExtraCheckDeshabilitado = false;
    }
  }

  
}

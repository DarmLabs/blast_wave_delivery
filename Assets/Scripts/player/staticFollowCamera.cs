using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staticFollowCamera : MonoBehaviour
{
    public GameObject Moto;
    void Update()
    {
        transform.Translate(Moto.transform.position.x ,0, playerMovement.speedz * (Time.deltaTime * playerMovement.speedMultiplier));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompSenoidal : MonoBehaviour
{
    public float anchoCiclo, frecuencia;
    float cX, contador, xSen;
    float velocidadEscaladoObs = 2;

    void Start() 
    {
        cX=transform.position.x;
    }
    void Update() 
    {
        contador=contador + ((frecuencia+(velocidadEscaladoObs*TileManager.escalado))/100);
        xSen = Mathf.Sin(contador);
        transform.position=new Vector3(cX+(xSen*anchoCiclo),transform.position.y,transform.position.z);
        if (contador > 6.28f)
        {
            contador=0;
        }
    }
}

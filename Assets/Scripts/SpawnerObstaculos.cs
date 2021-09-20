using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstaculos : MonoBehaviour
{
    public GameObject[] Obstaculos;
    public Transform[] spawnPoints;
    private int indexObstaculos = 6;
    private int indexPosicion = 6;
    public float timeSpawn = 0.4f;

    
    void Start () 
    {
        //se instancia la comida que cae, con una diferencia de tiempo, lo hace aleatoriamente
        InvokeRepeating("invocarComida", 0.1f, timeSpawn);
        
    }

    void Update()
    {
        
    }

    void invocarComida ()
    {
        Instantiate(Obstaculos[Random.Range(0,indexObstaculos)],spawnPoints[Random.Range(0,indexPosicion)].position,transform.rotation);
    }
   
}

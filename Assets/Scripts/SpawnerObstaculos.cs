using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstaculos : MonoBehaviour
{
    public GameObject[] ObstaculosVan;
    public GameObject[] ObstaculosVienen;
    public Transform[] spawnPointsVan;
    public Transform[] spawnPointsVienen;
    private int indexObstaculosVan = 3;
    private int indexObstaculosVienen = 3;
    private int indexPosicionVan = 3;
    private int indexPosicionVienen = 3;
    public float timeSpawn = 0.4f;

    
    void Start () 
    {
        //se instancia la comida que cae, con una diferencia de tiempo, lo hace aleatoriamente
        InvokeRepeating("invocarObstaculos", 0.1f, timeSpawn);
        
    }
    

    void invocarObstaculos ()
    {
        //Instantiate(Obstaculos[Random.Range(0,indexObstaculos)],spawnPoints[Random.Range(0,indexPosicion)].position,transform.rotation);
        Instantiate(ObstaculosVienen[Random.Range(0,indexObstaculosVienen)],spawnPointsVienen[Random.Range(0,indexPosicionVienen)].position,transform.rotation);
        Instantiate(ObstaculosVan[Random.Range(0,indexObstaculosVan)],spawnPointsVan[Random.Range(0,indexPosicionVan)].position,transform.rotation);
    }
   
}

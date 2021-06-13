using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    public GameObject coche1;
    public GameObject coche2;
    public GameObject coche3;
    public GameObject coche4;
    public GameObject coche5;
    public GameObject coche6;

    public float spawnTime;

    private GameObject prefabIzq;
    private GameObject prefabDer;

    // public GameObject prefabObstaculo;
    // private int cantidadSpawners;
    // Transform[] spawnersDisponibles;
    public GameObject Moto;
    void Start()
    {
        // spawn1 = GameObject.Find("s1");
        // spawn2 = GameObject.Find("s2");
        // spawn3 = GameObject.Find("s3");
        // spawn4 = GameObject.Find("s4");
        // spawnersDisponibles = gameObject.GetComponentsInChildren<Transform>();
        // cantidadSpawners = spawnersDisponibles.Length; // La primera entrada del arreglo es el objeto padre, tener en cuenta esto.

        // InvokeRepeating("spawnObstacle", 0.1f, spawnTime); //Llama al método que spawnea los obstaculos
        // InvokeRepeating("spawnObstacle", 0.15f, spawnTime); //Llama al método que spawnea los obstaculos

        InvokeRepeating("spawnIzq", 0.15f, spawnTime); //Llama al método que spawnea los obstaculos
        InvokeRepeating("spawnDer", 0.15f, spawnTime); //Llama al método que spawnea los obstaculos
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, Moto.GetComponent<playerMovementHorizontal>().speedz * (Time.deltaTime * Moto.GetComponent<playerMovementHorizontal>().speedMultiplier));
    }


    void spawnIzq(){
        var auxI = Random.Range(1, 3);

        if(auxI == 1){
            prefabIzq = coche1;
        };
        if(auxI == 2){
            prefabIzq = coche2;
        };
        if(auxI == 3){
            prefabIzq = coche3;
        };

        var auxIzq = Random.Range(1, 3);

        
        Debug.Log(auxIzq);

        if(auxIzq == 1){
            Instantiate(prefabIzq, new Vector3(spawn1.transform.position.x, spawn1.transform.position.y, spawn1.transform.position.z), Quaternion.identity); // Instancia el obstaculo en la posicion del spawner elegido
        }else{
            Instantiate(prefabIzq, new Vector3(spawn2.transform.position.x, spawn2.transform.position.y, spawn2.transform.position.z), Quaternion.identity); // Instancia el obstaculo en la posicion del spawner elegido
        }
    }

    void spawnDer(){
        var auxD = Random.Range(1, 3);

        if(auxD == 1){
            prefabDer = coche4;
        };
        if(auxD == 2){
            prefabDer = coche5;
        };
        if(auxD == 3){
            prefabDer = coche6;
        };

        var auxDer = Random.Range(1, 3);
        Debug.Log(auxDer);
        if(auxDer == 1){
            Instantiate(prefabDer, new Vector3(spawn3.transform.position.x, spawn3.transform.position.y, spawn3.transform.position.z), Quaternion.identity); // Instancia el obstaculo en la posicion del spawner elegido
        }else{
            Instantiate(prefabDer, new Vector3(spawn4.transform.position.x, spawn4.transform.position.y, spawn4.transform.position.z), Quaternion.identity); // Instancia el obstaculo en la posicion del spawner elegido
        }
    }

    // void spawnObstacle(){
    //     var spawnSeleccionado = Random.Range(1, cantidadSpawners); // Selecciono aleatoreamente el spawner
    //     var spawnLocation = spawnersDisponibles[spawnSeleccionado].gameObject.transform.position; // Selecciono la posicion del spawner elegido
    //     // var obstaculoPorInstanciar = prefabObstaculo; // Reservado para cuando tengamos que elegir entre múltiples obstaculos

    //     var aux = Random.Range(1, 6);
    //     Debug.Log(aux);
    //     if(aux == 1){
    //         prefabObstaculo = coche1;
    //     };
    //     if(aux == 2){
    //         prefabObstaculo = coche2;
    //     };
    //     if(aux == 3){
    //         prefabObstaculo = coche3;
    //     };
    //     if(aux == 4){
    //         prefabObstaculo = coche4;
    //     };
    //     if(aux == 5){
    //         prefabObstaculo = coche5;
    //     };
    //     if(aux == 6){
    //         prefabObstaculo = coche6;
    //     };

    //     Instantiate(prefabObstaculo, new Vector3(spawnLocation.x,spawnLocation.y,spawnLocation.z), Quaternion.identity); // Instancia el obstaculo en la posicion del spawner elegido
    // }
}
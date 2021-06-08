using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject coche1;
    public GameObject coche2;
    public GameObject coche3;
    public GameObject coche4;
    public GameObject coche5;

    private List<GameObject> arrayObs;
    public float spawnTime;
    public GameObject prefabObstaculo;
    private int cantidadSpawners;
    Transform[] spawnersDisponibles;
    public GameObject Moto;
    void Start()
    {
        spawnersDisponibles = gameObject.GetComponentsInChildren<Transform>();
        cantidadSpawners = spawnersDisponibles.Length; // La primera entrada del arreglo es el objeto padre, tener en cuenta esto.
        //Debug.Log(cantidadSpawners);
        //Debug.Log("%%%%%%%%%%%%%");

        InvokeRepeating("spawnObstacle", 0.0f, spawnTime); //Llama al método que spawnea los obstaculos
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, Moto.GetComponent<playerMovementHorizontal>().speedz * (Time.deltaTime * Moto.GetComponent<playerMovementHorizontal>().speedMultiplier));
    }

    void spawnObstacle(){
        var spawnSeleccionado = Random.Range(1, cantidadSpawners); // Selecciono aleatoreamente el spawner
        var spawnLocation = spawnersDisponibles[spawnSeleccionado].gameObject.transform.position; // Selecciono la posicion del spawner elegido
        // var obstaculoPorInstanciar = prefabObstaculo; // Reservado para cuando tengamos que elegir entre múltiples obstaculos

        var aux = Random.Range(1, 5);
        Debug.Log(aux);
        if(aux == 1){
            prefabObstaculo = coche1;
        };
        if(aux == 2){
            prefabObstaculo = coche2;
        };
        if(aux == 3){
            prefabObstaculo = coche3;
        };
        if(aux == 4){
            prefabObstaculo = coche4;
        };
        if(aux == 5){
            prefabObstaculo = coche5;
        };

        Instantiate(prefabObstaculo, new Vector3(spawnLocation.x,spawnLocation.y,spawnLocation.z), Quaternion.identity); // Instancia el obstaculo en la posicion del spawner elegido
    }
}
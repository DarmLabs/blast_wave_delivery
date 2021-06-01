using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float spawnTime;
    public GameObject prefabObstaculo;
    private int cantidadSpawners;
    Transform[] spawnersDisponibles;
    public GameObject Moto;
    void Start()
    {
        spawnersDisponibles = gameObject.GetComponentsInChildren<Transform>();
        //Debug.Log(spawnersDisponibles.Length);
        cantidadSpawners = spawnersDisponibles.Length; // La primera entrada del arreglo es el objeto padre, tener en cuenta esto.

        InvokeRepeating("spawnObstacle", 0.1f, spawnTime); //Llama al método que spawnea los obstaculos
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
        Instantiate(prefabObstaculo, new Vector3(spawnLocation.x,spawnLocation.y,spawnLocation.z), Quaternion.identity); // Instancia el obstaculo en la posicion del spawner elegido
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject Moto;

    //Variables Recolectables
        //Pizzas
    public GameObject [] spawnsP;
    public int SpawnerP;
    int cantPizzas;
    int loop;
    public GameObject pizzaPrefab;
        //Combustible
    public GameObject [] spawnsF;
    public int SpawnerF;
    int waitingTime;
    public GameObject fuelPrefab;
    bool spawnFactive = false;
    void Start()
    {
        //for Coins
        spawnsP = GameObject.FindGameObjectsWithTag("coinSpawner");
        selectPSpawner();
        //for Fuel
        spawnsF = GameObject.FindGameObjectsWithTag("fuelSpawner");
    }
    void Update()
    {
        transform.Translate(0,0, Moto.GetComponent<playerMovement>().speedz * (Time.deltaTime * Moto.GetComponent<playerMovement>().speedMultiplier));
        selectFSpawner();
    }
    //Spawns Pizzas
    void selectPSpawner()
    {
        SpawnerP = Random.Range(0, spawnsP.Length);
        cantPizzas = Random.Range(3,6);
        spawnCoin();
    }
    void spawnCoin()
    {
        Instantiate(pizzaPrefab, spawnsP[SpawnerP].transform.position, Quaternion.identity);
        StartCoroutine(timeBetweenPizzas(0.2f));
    }
    IEnumerator timeBetweenPizzas(float secs)
    {
        yield return new WaitForSeconds(secs);
        if(loop < cantPizzas)
        {
            spawnCoin();
            loop = loop +1;
        }
        else
        {
            loop = 0;
            selectPSpawner();
        }
    }
    IEnumerator timeBetweenRows(float secs)
    {
        yield return new WaitForSeconds(secs);
        selectPSpawner();
    }
    //Spawn Fuel
    void selectFSpawner()
    {
        if(Moto.GetComponent<playerProprieties>().currentFuel < 200 && !spawnFactive)
        {
            Debug.Log("seleccionoSpawner");
            spawnFactive = true;
            SpawnerF = Random.Range(0, 2);
            waitingTime = Random.Range(10,15);
            StartCoroutine(timeBetweenSpawn(waitingTime));
        }
    }
    IEnumerator timeBetweenSpawn(int secs)
    {
        yield return new WaitForSeconds(secs);
        Debug.Log("spawneo");
        Instantiate(fuelPrefab, spawnsF[SpawnerF].transform.position, Quaternion.identity);
        spawnFactive = false;
    }
}
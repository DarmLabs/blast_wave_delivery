using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0f;
    private float tileLength = 30f;
    private int amnTilesOnScreen = 2;
    private GameObject go;
    private int lastPrefabIndex = 0;
    private int randomIndex =0;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i=0; i<amnTilesOnScreen;i++){
            SpawnTile();
        }
    }

    void Update()
    {
        if(playerTransform.position.z > (spawnZ - amnTilesOnScreen*tileLength)){
            SpawnTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position=Vector3.forward*spawnZ;
        spawnZ+=tileLength;
    }

    private int RandomPrefabIndex()
    {
        if(tilePrefabs.Length<=1)
        return 0;
        int randomIndex = lastPrefabIndex;
        while(randomIndex==lastPrefabIndex)
        {
            randomIndex =Random.Range(0,tilePrefabs.Length);            
        }
        lastPrefabIndex=randomIndex;
        return randomIndex;
    }
    
}

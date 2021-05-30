using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0f;
    private float tileLength = 30f;
    private int amnTilesOnScreen = 5;
    private GameObject go;
    private int lastPrefabIndex = 0;
    private bool visualSet1 = true;
    private bool visualSet2=false, visualSet3=false, visualSet4=false, visualSet5 = false; 
    private float checkPointsCounter = 0f;    
  
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
            //randomIndex =Random.Range(0,tilePrefabs.Length);
            if (visualSet1)
            {
                randomIndex = Random.Range(0,2);
            }
             if (visualSet2)
            {
                randomIndex = Random.Range(2,4);
            }
             if (visualSet3)
            {
                randomIndex = Random.Range(4,6);
            }
             if (visualSet4)
            {
                randomIndex = Random.Range(6,8);
            }
             if (visualSet5)
            {
                randomIndex = Random.Range(8,10);
            }
                       
        }
        lastPrefabIndex=randomIndex;
        return randomIndex;
    }

    private void visualChange()
    {
        if (checkPointsCounter==0f)
        {
            visualSet1=true;
            visualSet2=false;
            visualSet3=false;
            visualSet4=false;
            visualSet5=false;
        }
        
    }

    /*
    private void MoveTile()
    {

    }
    */
}

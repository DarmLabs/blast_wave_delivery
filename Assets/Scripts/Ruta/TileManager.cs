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
    private bool visualSet1=false, visualSet2=false, visualSet3=false, visualSet4=false, visualSet5 = false; 
    private int checkPointsCounter = 0;
       
  
    void Start()
    {
        visualSet1=true;
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
        visualChange();

    }

    private void SpawnTile(int prefabIndex = -1)
    {
        go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position=Vector3.forward*spawnZ;
        spawnZ+=tileLength;
        checkPointsCounter+=1;
        Debug.Log("tiles:"+checkPointsCounter);
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
        if (checkPointsCounter==0)
        {
            visualSet1=true;
            visualSet2=false;
            visualSet3=false;
            visualSet4=false;
            visualSet5=false;
            Debug.Log("SET 1");
        }
        if (checkPointsCounter==10)
        {
            visualSet1=false;
            visualSet2=true;
            visualSet3=false;
            visualSet4=false;
            visualSet5=false;
            Debug.Log("SET 2");
        }
        if (checkPointsCounter==20)
        {
            visualSet1=false;
            visualSet2=false;
            visualSet3=true;
            visualSet4=false;
            visualSet5=false;
            Debug.Log("SET 3");
        }
        if (checkPointsCounter==30)
        {
            visualSet1=false;
            visualSet2=false;
            visualSet3=false;
            visualSet4=true;
            visualSet5=false;
            Debug.Log("SET 4");
        }
        if (checkPointsCounter==40)
        {
            visualSet1=false;
            visualSet2=false;
            visualSet3=false;
            visualSet4=false;
            visualSet5=true;
            Debug.Log("SET 5");
        }        
    }

    /*
    private void MoveTile()
    {

    }
    */
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region Variables
    public GameObject[] tilePrefabs;
    private Transform playerTransform;
    private float spawnZ = 0f;
    private float tileLength = 60f;
    private int amnTilesOnScreen = 10;
    private GameObject go;
    private int lastPrefabIndex = 0;    
    private bool visualSet1=false, visualSet2=false, visualSet3=false, visualSet4=false, visualSet5 = false; 
    private int tileCounter = 0;
    private int contadorHastaCheck = 0;
    private int checkpointCounter = 0;
    #endregion   
  
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

        //ACA SE ACTIVA EL CHECKPOINT
        if (contadorHastaCheck == Random.Range(9,14))
        {
           go.transform.GetChild(1).gameObject.SetActive(true);
           checkpointCounter+=1;
           contadorHastaCheck=0;
           Debug.Log("Checkpoint Activo:"+contadorHastaCheck+"tiles:"+tileCounter);
        }
        

        go.transform.SetParent(transform);
        go.transform.position=Vector3.forward*spawnZ;
        spawnZ+=tileLength;
        tileCounter+=1;
        contadorHastaCheck +=1;
        //Debug.Log("tiles:"+tileCounter);
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
                randomIndex = Random.Range(0,4);
            }
             if (visualSet2)
            {
                randomIndex = Random.Range(5,9);
            }
             if (visualSet3)
            {
                randomIndex = Random.Range(10,14);
            }
             if (visualSet4)
            {
                randomIndex = Random.Range(15,19);
            }                
            if (visualSet5)
            {
                randomIndex = Random.Range(20,24);
            }
                       
        }
        lastPrefabIndex=randomIndex;
        return randomIndex;
    }

    private void visualChange()
    {
        if (tileCounter==0)
        {
            visualSet1=true;
            visualSet2=false;
            visualSet3=false;
            visualSet4=false;
            visualSet5=false;
            //Debug.Log("SET 1");
        }
        if (tileCounter==15)
        {
            visualSet1=false;
            visualSet2=true;
            visualSet3=false;
            visualSet4=false;
            visualSet5=false;
            //Debug.Log("SET 2");
        }
        if (tileCounter==30)
        {
            visualSet1=false;
            visualSet2=false;
            visualSet3=true;
            visualSet4=false;
            visualSet5=false;
            //Debug.Log("SET 3");
        }
        if (tileCounter==45)
        {
            visualSet1=false;
            visualSet2=false;
            visualSet3=false;
            visualSet4=true;
            visualSet5=false;
            //Debug.Log("SET 4");
        }
        if (tileCounter==100)
        {
            visualSet1=false;
            visualSet2=false;
            visualSet3=false;
            visualSet4=false;
            visualSet5=true;
            //Debug.Log("SET 5");
        }        
    }

    /*
    private void MoveTile()
    {

    }
    */
}

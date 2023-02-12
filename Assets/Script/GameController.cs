using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public List<GameObject> redPooledGO;
    public List<GameObject> bluePooledGO;

    [Header("ObstacleHandler")]
    [SerializeField] Transform[] obstacleSpawners;
    [SerializeField] GameObject obstacleSpawnPrefab;
    float timer;

    void Start()
    {
        instance = this;
        redPooledGO = new List<GameObject>();
        bluePooledGO = new List<GameObject>();

        SetTimer();
    }

    private void Update() {
        ObstacleSpawnTimer();
    }

#region Object Pooling
    public GameObject GetGameObjectInPool(string wallTagName){
        GameObject returnGameObject = null;
 
        if(wallTagName == "RedWall" && redPooledGO.Count > 0){
            returnGameObject = redPooledGO[0];
            returnGameObject.SetActive(true);
            redPooledGO.RemoveAt(0);
            return returnGameObject;
        }else if(wallTagName == "BlueWall" && bluePooledGO.Count > 0){
            returnGameObject = bluePooledGO[0];
            returnGameObject.SetActive(true);
            bluePooledGO.RemoveAt(0);
            return returnGameObject;
        }
 
        return returnGameObject;
    }
#endregion

#region ObstacleHandler
    void SetTimer(){
        timer = Random.Range(3, 8);
    }

    void ObstacleSpawnTimer(){
        if(timer <= 0){
            Instantiate(obstacleSpawnPrefab, obstacleSpawners[Random.Range(0, obstacleSpawners.Length)].position, Quaternion.identity);
            timer = Random.Range(3, 8);
        }else{
            timer -= Time.deltaTime;
        }
    }
#endregion

}

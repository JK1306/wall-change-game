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
    [SerializeField] ObjectPooling objectPooling;
    List<GameObject> spawnedObstacles;
    float timer;

    [Header("SFX")]
    [SerializeField] AudioClip deathSfx;

    void Start()
    {
        instance = this;
        redPooledGO = new List<GameObject>();
        bluePooledGO = new List<GameObject>();
        spawnedObstacles = new List<GameObject>();

        SetTimer();
    }

    private void Update() {
        ObstacleSpawnTimer();
        DisableObstacles();
    }

#region ObstacleHandler
    void SetTimer(){
        timer = Random.Range(3, 8);
    }

    void ObstacleSpawnTimer(){
        if(timer <= 0){
            GameObject spawnedObstacle;
            spawnedObstacle = objectPooling.GetObjectFromPool(obstacleSpawnPrefab);
            spawnedObstacle.SetActive(true);
            spawnedObstacle.transform.position = obstacleSpawners[Random.Range(0, obstacleSpawners.Length)].position;
            spawnedObstacles.Add(
                spawnedObstacle
                // (GameObject)Instantiate(obstacleSpawnPrefab, obstacleSpawners[Random.Range(0, obstacleSpawners.Length)].position, Quaternion.identity)
            );
            timer = Random.Range(3, 8);
        }else{
            timer -= Time.deltaTime;
        }
    }

    void DisableObstacles(){
        if(spawnedObstacles.Count > 0){
            for (int i=0; i<spawnedObstacles.Count; i++)
            {
                if(Vector3.Distance(spawnedObstacles[i].transform.position, obstacleSpawners[0].position) >= 10f){
                    objectPooling.AddToPool(spawnedObstacles[i]);
                    spawnedObstacles.RemoveAt(i);
                }
            }
        }
    }
#endregion

    public void PlayDeathSFX(){
        gameObject.GetComponent<AudioSource>().clip = deathSfx;
        gameObject.GetComponent<AudioSource>().Play();
    }

}

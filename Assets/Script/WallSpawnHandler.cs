using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnHandler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject[] walls;
    [SerializeField] ObjectPooling objectPooling;
    List<GameObject> childWalls = new List<GameObject>();
    GameObject lastElement, spawnObject;
    int currentPositionY;

    void Start()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++){
            childWalls.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        if(((int)playerTransform.position.y % 10) == 0 && currentPositionY != (int)playerTransform.position.y){
            currentPositionY = (int)playerTransform.position.y;
            lastElement = this.childWalls[this.childWalls.Count - 1];
            spawnObject = walls[Random.Range(0,walls.Length)];
            GameObject wall = objectPooling.GetObjectFromPool(spawnObject);
            wall.SetActive(true);
            wall.transform.position = new Vector3(
                                        lastElement.transform.position.x, 
                                        lastElement.transform.position.y + 10, 
                                        lastElement.transform.position.z
                                    );

            wall.transform.SetParent(transform);

            this.childWalls.RemoveAt(0);
            this.childWalls.Add(wall);
            objectPooling.AddToPool(gameObject.transform.GetChild(0).gameObject);
        }
    }
}

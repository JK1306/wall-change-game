using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnHandler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject[] walls;
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
            GameObject wall = GameController.instance.GetGameObjectInPool(spawnObject.tag);
            
            if(wall == null){
                wall = Instantiate(
                    spawnObject,
                    new Vector3(
                        lastElement.transform.position.x, 
                        lastElement.transform.position.y + 10, 
                        lastElement.transform.position.z
                    ), 
                    Quaternion.identity
                );
            }

            wall.transform.SetParent(transform);

            this.childWalls.Add(wall);
            this.childWalls.RemoveAt(0);
            Destroy(gameObject.transform.GetChild(0).gameObject);
            // if(lastElement.tag == "RedWall"){
            //     GameController.instance.redPooledGO.Add(lastElement);
            // }else{
            //     GameController.instance.bluePooledGO.Add(lastElement);
            // }
            // this.childWalls[0].SetActive(false);
            // // this.childWalls[0].transform.parent = null;
            // this.childWalls.RemoveAt(0);
        }
    }
}

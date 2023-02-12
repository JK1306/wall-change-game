using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawnHandler : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject[] walls;
    List<GameObject> childWalls = new List<GameObject>();
    GameObject lastElement;

    void Start()
    {
        Debug.Log(gameObject.name+" Child Count : "+gameObject.transform.childCount);
        for(int i = 0; i < gameObject.transform.childCount; i++){
            childWalls.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        if(((int)playerTransform.position.y % 10) == 0){
            lastElement = childWalls[childWalls.Count - 1];
            GameObject wall = Instantiate(
                walls[Random.Range(0,walls.Length)], 
                new Vector3(
                    lastElement.transform.position.x, 
                    lastElement.transform.position.y + 10, 
                    lastElement.transform.position.z
                ), 
                Quaternion.identity
            );

            wall.transform.SetParent(transform);

            childWalls.Add(wall);
        }
    }
}

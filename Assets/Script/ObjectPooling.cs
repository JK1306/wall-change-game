using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] List<GameObject> pooledObject;
    GameObject returnObject;
    void Start()
    {
        pooledObject = new List<GameObject>();
    }

    public void AddToPool(GameObject poolObject){
        poolObject.transform.parent = null;
        poolObject.SetActive(false);
        pooledObject.Add(poolObject);
    }
    
    public GameObject GetObjectFromPool(GameObject prefabObject){
        returnObject = null;

        if(pooledObject.Count > 0){
            for(int i = 0; i < pooledObject.Count; i++){
                if(pooledObject[i].name.Contains(prefabObject.name)){
                    returnObject = pooledObject[i];
                    pooledObject.RemoveAt(i);
                    break;
                }
            }
        }

        if(returnObject == null){
            returnObject = Instantiate(prefabObject);
        }
        return returnObject;
    }
}

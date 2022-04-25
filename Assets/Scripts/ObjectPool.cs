using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance; //Object Pool Instance
    public List<GameObject> pooledObjects; //List of instantiated GameObjects
    public GameObject pooledObject; //The GameObject to be pooled
    public int poolSize; //Size of the pool.


    private void Awake()
    {
        SharedInstance = this;
    }

       public GameObject GetPooledObject()
    {
       
        for (int i = 0; i < poolSize; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                Debug.Log(pooledObject.name + " retrieved from pool.");
                return pooledObjects[i];
            }
        }
        Debug.Log(pooledObject.name + " has reached max active capacity.");
        return null;
    }

    public ObjectPool(GameObject obj, int amount)
    {
        this.pooledObject = obj;
        this.poolSize = amount;

        pooledObjects = new List<GameObject>();
        GameObject tmp;

        //Create object pool by initializing a specified amount of copies of the same object.
        for (int i = 0; i < this.poolSize; i++)
        {
            tmp = Instantiate(this.pooledObject);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
}

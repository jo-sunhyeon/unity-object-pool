using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject GetObject() 
    {
        if (inactiveObjects.Count > 0) 
        {
            GameObject dequeuedObject = inactiveObjects.Dequeue();
            dequeuedObject.transform.SetParent(null);
            dequeuedObject.SetActive(true);
            return dequeuedObject;
        }
        else 
        {
            GameObject newObject = Instantiate(Prefab);
            PooledObject pooledObject = newObject.AddComponent<PooledObject>();
            pooledObject.owner = this;
            return newObject;
        }
    }

    public void ReturnObject(GameObject toReturn) 
    {
        if (toReturn == null)
        {
            throw new ArgumentNullException();
        }
        if (toReturn.GetComponent<PooledObject>() == null)
        {
            throw new ArgumentException();
        }
        toReturn.transform.SetParent(transform);
        toReturn.SetActive(false);
        inactiveObjects.Enqueue(toReturn);
    }
    
    public GameObject Prefab;
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();
}
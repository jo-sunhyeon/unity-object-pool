using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject GetObject() 
    {
        if (inactiveObjects.Count > 0) 
        {
            var dequeuedObject = inactiveObjects.Dequeue();
            dequeuedObject.transform.SetParent(null);
            dequeuedObject.SetActive(true);
            return dequeuedObject;
        }
        else 
        {
            var newObject = Instantiate(Prefab);
            var pooledObject = newObject.AddComponent<PooledObject>();
            pooledObject.owner = this;
            return newObject;
        }
    }

    public void DestroyOrReturnObject(GameObject gameObject) 
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException();
        }
        if (gameObject.GetComponent<PooledObject>() == null)
        {
            Destroy(gameObject);
            return;
        }
        gameObject.transform.SetParent(transform);
        gameObject.SetActive(false);
        inactiveObjects.Enqueue(gameObject);
    }
    
    public GameObject Prefab;
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();
}
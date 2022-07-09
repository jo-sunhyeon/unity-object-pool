// References:
// https://learn.unity.com/tutorial/live-training-shop-ui-with-runtime-scroll-lists#5c7f8528edbc2a002053b4c8
// Paris Buttfield-Addison, Jon Manning, and Tim Nugent. Unity Game Development Cookbook: Essentials for Every Game. O'Reilly Media, 2019

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
            return Instantiate(Prefab);
        }
    }

    public void ReturnObject(GameObject toReturn) 
    {
        if (toReturn == null)
        {
            throw new ArgumentNullException();
        }
        toReturn.transform.SetParent(transform);
        toReturn.SetActive(false);
        inactiveObjects.Enqueue(toReturn);
    }
    
    public GameObject Prefab;
    private Queue<GameObject> inactiveObjects = new Queue<GameObject>();
}
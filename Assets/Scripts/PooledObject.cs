using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class PooledObject : MonoBehaviour
{
    public ObjectPool owner;
}

public static class PooledObjectExtension
{
    public static void ReturnOrDestroy(this GameObject gameObject)
    {
        if (gameObject == null)
        {
            throw new ArgumentNullException();
        }
        var pooledObject = gameObject.GetComponent<PooledObject>();
        if (pooledObject != null)
        {
            pooledObject.owner.ReturnObject(gameObject);
            return;
        }
        Object.Destroy(gameObject);
    }
}
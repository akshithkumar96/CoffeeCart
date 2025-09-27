using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : IPool
{
    private GameObject prefab;
    private Queue<GameObject> pool = new Queue<GameObject>();
    private List<GameObject> activeObjects = new List<GameObject>();
    
    public ObjectPool(GameObject prefab, int initialSize = 10)
    {
        this.prefab = prefab;
        InitializePool(initialSize);
    }
    
    private void InitializePool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Object.Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    
    public GameObject GetItem()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            activeObjects.Add(obj);
            return obj;
        }
        else
        {
            GameObject newObj = Object.Instantiate(prefab);
            newObj.SetActive(true);
            activeObjects.Add(newObj);
            return newObj;
        }
    }
    
    public void ReleaseItem(GameObject gameObject)
    {
        if (activeObjects.Contains(gameObject))
        {
            activeObjects.Remove(gameObject);
            gameObject.SetActive(false);
            pool.Enqueue(gameObject);
        }
    }
    
    public void ReleaseAll()
    {
        foreach (GameObject obj in activeObjects)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
        activeObjects.Clear();
    }
    
    public int GetActiveCount()
    {
        return activeObjects.Count;
    }
    
    public int GetPooledCount()
    {
        return pool.Count;
    }
}

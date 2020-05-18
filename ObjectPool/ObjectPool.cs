using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Object
{
    private readonly Queue<T> _pool;
    private readonly List<T> _prefab;

    public ObjectPool(IEnumerable<T> poolPrefab)
    {
        _pool = new Queue<T>();
        _prefab = new List<T>();
        _prefab.AddRange(poolPrefab);
    }

    /*public ObjectPool(IEnumerable<T> prefabs)
    {
        _pool = new Queue<T>();
        _prefab.AddRange(prefabs);
    }*/

    public void Pool(T objectToPool)
    {
        _pool.Enqueue(objectToPool);
    }

    public T DePool()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }

        var randomNumber = Random.Range(0, _prefab.Count);
        return Object.Instantiate(_prefab[randomNumber]);
    }

    public void PoolAll(IEnumerable<T> listOfObject)
    {
        foreach (var obj in listOfObject)
        {
            Pool(obj);
        }
    }
}
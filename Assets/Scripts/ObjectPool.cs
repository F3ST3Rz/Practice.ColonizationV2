using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] private Transform _container;
    [SerializeField] private Box _prefab;

    private Queue<Box> _pool;

    public IEnumerable<Box> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<Box>();
    }

    public Box GetObject()
    {
        if (_pool.Count == 0)
        {
            var box = Instantiate(_prefab);
            box.transform.parent = _container;

            return box;
        }

        return _pool.Dequeue();
    }

    public void PutObject(Box box)
    {
        _pool.Enqueue(box);
        box.gameObject.SetActive(false);
        box.transform.parent = _container;
    }

    public Transform GetTransformContainer()
    {
        return _container;
    }
}

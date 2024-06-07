using UnityEngine;
using System.Collections.Generic;

public class Scanner : MonoBehaviour
{
    private const string Spawner = nameof(Spawner);

    private Transform _targetScan;
    private Dictionary<Box, bool> _boxes;

    private void Awake()
    {
        if(GameObject.Find(Spawner).TryGetComponent(out ObjectPool objectPool))
            _targetScan = objectPool.TransformContainer;
    }

    private void Start()
    {
        _boxes = new Dictionary<Box, bool>();
        SearchBoxes();
    }

    private void SearchBoxes()
    {
        int countBoxes = _targetScan.childCount;

        for (int i = 0; i < countBoxes; i++)
        {
            if (_targetScan.GetChild(i).TryGetComponent(out Box box) 
                && _boxes.ContainsKey(box) == false
                && box.isActiveAndEnabled)
            {
                _boxes.Add(box, true);
            }
        }
    }

    private void SetBusyBox(Box box)
    {
        _boxes[box] = false;
    }

    public Transform GetObjectScan()
    {
        foreach (KeyValuePair<Box, bool> pair in _boxes)
        {
            if (pair.Value)
            {
                SetBusyBox(pair.Key);
                return pair.Key.transform;
            }
        }

        SearchBoxes();
        return null;
    }

    public void RemoveBox(Box box)
    {
        _boxes.Remove(box);
    }
}

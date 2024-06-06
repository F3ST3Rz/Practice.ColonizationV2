using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    private const string Spawner = nameof(Spawner);

    private Transform _targetScan;

    private void Awake()
    {
        _targetScan = GameObject.Find(Spawner).GetComponent<ObjectPool>().GetTransformContainer();
    }

    public Transform GetObjectScan()
    {
        int countBoxes = _targetScan.childCount;

        for (int i = 0; i < countBoxes; i++)
        {
            if (_targetScan.GetChild(i).GetComponent<Box>().IsDetected == false && _targetScan.GetChild(i).GetComponent<Box>().isActiveAndEnabled)
            {
                _targetScan.GetChild(i).GetComponent<Box>().SetDetected();
                return _targetScan.GetChild(i);
            }
        }

        return null;
    }
}

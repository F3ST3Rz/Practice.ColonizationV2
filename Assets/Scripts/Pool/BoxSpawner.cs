using System.Collections;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private ObjectPool _pool;

    private Transform[] _transformPoints;
    private int _countPoints;

    private void Start()
    {
        _countPoints = transform.childCount;
        _transformPoints = new Transform[_countPoints];

        if (_countPoints == 0)
            return;

        for (int i = 0; i < _countPoints; i++)
        {
            _transformPoints[i] = transform.GetChild(i);
        }

        StartCoroutine(GenerateBox());
    }

    private IEnumerator GenerateBox()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Vector3 spawnPoint = _transformPoints[Random.Range(0, _countPoints)].position;
        var box = _pool.GetObject();
        box.gameObject.SetActive(true);
        box.transform.position = spawnPoint;
    }
}

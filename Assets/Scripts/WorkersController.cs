using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Base))]
[RequireComponent(typeof(Scanner))]
[RequireComponent(typeof(Storager))]
public class WorkersController : MonoBehaviour
{
    [SerializeField] private Worker _prefab;

    private Scanner _scanner;
    private Base _base;
    private Storager _storager;
    private List<Worker> _workers;
    private int _workersCount;
    private bool _isBuild = false;

    private void Awake()
    {
        _storager = GetComponent<Storager>();
    }

    private void Start()
    {
        _base = GetComponent<Base>();
        _scanner = GetComponent<Scanner>();
        _workersCount = transform.childCount;
        _workers = new List<Worker>(_workersCount);

        for (int i = 0; i < _workersCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Worker>())
                _workers.Add(transform.GetChild(i).GetComponent<Worker>());
        }
    }

    private void OnEnable()
    {
        _storager.WorkerAdded += AddWorker;
        _storager.BaseAdded += SetBuild;
    }

    private void OnDisable()
    {
        _storager.WorkerAdded -= AddWorker;
        _storager.BaseAdded -= SetBuild;
    }

    private Worker GetFreeWorker()
    {
        foreach (Worker worker in _workers)
        {
            if (worker.IsWork == false && worker.IsBuild == false)
                return worker;
        }

        return null;
    }

    private void SetBuild()
    {
        _isBuild = true;
    }

    private void Update()
    {
        if (GetFreeWorker() == null)
            return;

        Transform transformObjectScan = _scanner.GetObjectScan();

        if (transformObjectScan == null)
            return;

        Worker worker = GetFreeWorker();

        if (_isBuild)
        {
            worker.Build(_base.TransformFlag);
            _isBuild = false;
            return;
        }

        worker.GetTarget(transformObjectScan);
    }

    private void AddWorker()
    {
        Vector3 spawnPoint = transform.position;
        var worker = Instantiate(_prefab);
        worker.transform.parent = transform;
        worker.transform.position = spawnPoint;
        _workers.Add(worker);
    }
}

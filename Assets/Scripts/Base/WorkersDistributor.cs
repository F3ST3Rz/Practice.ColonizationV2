using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Adder), typeof(Base), typeof(Scanner))]
public class WorkersController : MonoBehaviour
{
    [SerializeField] private Worker _prefab;

    private Scanner _scanner;
    private Base _base;
    private Adder _adder;
    private List<Worker> _workers;
    private int _workersCount;
    private bool _isBuild = false;

    private void Awake()
    {
        _adder = GetComponent<Adder>();
    }

    private void OnEnable()
    {
        _adder.WorkerAdded += AddWorker;
        _adder.BaseAdded += SetBuild;
    }

    private void OnDisable()
    {
        _adder.WorkerAdded -= AddWorker;
        _adder.BaseAdded -= SetBuild;
    }

    private void Start()
    {
        _base = GetComponent<Base>();
        _scanner = GetComponent<Scanner>();
        _workersCount = transform.childCount;
        _workers = new List<Worker>(_workersCount);

        for (int i = 0; i < _workersCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Worker worker))
                _workers.Add(worker);
        }
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

    private void AddWorker()
    {
        Vector3 spawnPoint = transform.position;
        var worker = Instantiate(_prefab);
        worker.transform.parent = transform;
        worker.transform.position = spawnPoint;
        _workers.Add(worker);
    }
}

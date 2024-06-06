using UnityEngine;
using System;

[RequireComponent(typeof(Base))]
public class Storager : MonoBehaviour
{
    private const string Spawner = nameof(Spawner);

    [SerializeField] private int _priceNewWorker;
    [SerializeField] private int _priceNewBase;

    private int _countBoxes;
    private Base _base;
    private bool _isBuild = false;
    private ObjectPool _pool;

    public event Action<int> CountBoxesChanged;
    public event Action WorkerAdded;
    public event Action BaseAdded;

    private void Awake()
    {
        _base = GetComponent<Base>();
        _pool = GameObject.Find(Spawner).GetComponent<ObjectPool>();
    }

    private void Start()
    {
        _countBoxes = 0;
    }

    private void OnEnable()
    {
        _base.BuildedBase += SetBuild;
    }

    private void OnDisable()
    {
        _base.BuildedBase -= SetBuild;
    }

    private void SetBuild()
    {
        _isBuild = true;
    }

    private void AddBox()
    {
        _countBoxes++;
        BuyWorker();
        BuyBase();
        CountBoxesChanged?.Invoke(_countBoxes);
    }

    private void BuyWorker()
    {
        if (_isBuild)
            return;

        if (_countBoxes >= _priceNewWorker)
        {
            WorkerAdded?.Invoke();
            _countBoxes -= _priceNewWorker;
        }    
    }

    private void BuyBase()
    {
        if (_isBuild == false)
            return;

        if (_countBoxes >= _priceNewBase)
        {
            BaseAdded?.Invoke();
            _countBoxes -= _priceNewBase;
            _isBuild = false;
        }
    }

    public void PutUp(Worker worker, Box box)
    {
        worker.Reset();
        box.Reset();
        _pool.PutObject(box);
        AddBox();
    }  
}

using UnityEngine;
using System;

[RequireComponent(typeof(Storager), typeof(Base))]
public class Adder : MonoBehaviour
{
    [SerializeField] private int _priceNewWorker;
    [SerializeField] private int _priceNewBase;

    private bool _isBuild = false;
    private Storager _storager;
    private Base _base;

    public event Action<int> BoxesSpended;
    public event Action WorkerAdded;
    public event Action BaseAdded;

    private void Awake()
    {
        _base = GetComponent<Base>();
        _storager = GetComponent<Storager>();
    }

    private void OnEnable()
    {
        _base.BuildedBase += SetBuild;
        _storager.CountBoxesChanged += Spend;
    }

    private void OnDisable()
    {
        _base.BuildedBase -= SetBuild;
        _storager.CountBoxesChanged -= Spend;
    }

    private void SetBuild()
    {
        _isBuild = true;
    }

    private void Spend(int countBoxes)
    {
        SpendOnWorker(countBoxes);
        SpendOnBase(countBoxes);
    }

    private void SpendOnWorker(int countBoxes)
    {
        if (_isBuild)
            return;

        if (countBoxes >= _priceNewWorker)
        {
            WorkerAdded?.Invoke();
            BoxesSpended?.Invoke(_priceNewWorker);
        }
    }

    private void SpendOnBase(int countBoxes)
    {
        if (_isBuild == false)
            return;

        if (countBoxes >= _priceNewBase)
        {
            BaseAdded?.Invoke();
            BoxesSpended?.Invoke(_priceNewBase);
            _isBuild = false;
        }
    }
}

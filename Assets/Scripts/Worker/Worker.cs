using System;
using UnityEngine;

[RequireComponent(typeof(Getter), typeof(Mover))]
public class Worker : MonoBehaviour
{
    private Getter _getter;
    private Mover _mover;

    private bool _isWork = false;
    private bool _isBuild = false;

    public event Action<Transform> TargetReceived;
    public event Action<Transform> TargetBuilded;
    public bool IsWork => _isWork;
    public bool IsBuild => _isBuild;

    private void Start()
    {
        _getter = GetComponent<Getter>();
        _mover = GetComponent<Mover>();
    }

    public void Build(Transform target)
    {
        _isBuild = true;
        TargetBuilded?.Invoke(target);
        GetTarget(target);

    }

    public void GetTarget(Transform target)
    {
        TargetReceived?.Invoke(target);
        _isWork = true;
    }

    public void Reset()
    {
        _isWork = false;
        _isBuild = false;
        _getter.Reset();
        _mover.Reset();
    }
}

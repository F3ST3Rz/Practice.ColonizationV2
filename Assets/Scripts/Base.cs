using UnityEngine;
using System;

[RequireComponent(typeof(Storager))]
public class Base : MonoBehaviour
{
    [SerializeField] private Flag _flag;

    private Storager _storager;
    private bool _isBuild = false;

    public event Action BuildedBase;
    public Transform TransformFlag => _flag.transform;

    private void Awake()
    {
        _storager = GetComponent<Storager>();
    }

    private void OnEnable()
    {
        _storager.BaseAdded += SetBuild;
    }

    private void OnDisable()
    {
        _storager.BaseAdded -= SetBuild;
    }

    private void SetBuild()
    {
        _isBuild = true;
    }

    public void SetFlag(Vector3 position)
    {
        if (_isBuild) 
            return;

        float radius = 5f; 
        float maxDistance = 0.2f; 
        bool isFlat = true;

        for (float angle = 0f; angle < 360f; angle += 45f)
        {
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward;
            RaycastHit hit;

            if (Physics.Raycast(position, dir, out hit, radius))
            {
                if (hit.distance > maxDistance) 
                {
                    isFlat = false;
                    return; 
                }
            }
        }

        if (isFlat)
        {
            _flag.transform.position = position;

            if (_flag.isActiveAndEnabled == false)
            {
                _flag.SetEnable();
                BuildedBase?.Invoke();
            }
        }
    }

    public void Reset()
    {
        _isBuild = false;
        _flag.SetDisable();
    }
}

using UnityEngine;

[RequireComponent (typeof(Mover), typeof(Worker))]
public class Builder : MonoBehaviour
{
    [SerializeField] private Base _prefab;
    [SerializeField] private Vector3 _offsetSpawn;

    private Worker _worker;
    private Mover _mover;
    private bool _isBuild = false;
    private Transform _target;

    private void Awake()
    {
        _worker = GetComponent<Worker>();
    }

    private void OnEnable()
    {
        _worker.TargetBuilded += SetBuild;
    }

    private void OnDisable()
    {
        _worker.TargetBuilded -= SetBuild;
    }

    private void Start()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        if (_isBuild == false)
            return;

        Build();
    }

    private void SetBuild(Transform target)
    {
        _isBuild = true;
        _target = target;
    }

    private void Build()
    {
        if(transform.position == _target.position)
        {
            var newBase = Instantiate(_prefab);
            newBase.transform.position = _target.position;
            gameObject.GetComponentInParent<Base>().Reset();
            gameObject.transform.parent = newBase.transform;
            gameObject.transform.position = newBase.transform.position + _offsetSpawn;
            _mover.SetTransformBase(newBase.transform);
            _isBuild = false;
            _worker.Reset();
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Worker))]
[RequireComponent(typeof(Getter))]
public class Mover : MonoBehaviour
{
    private const string Walk = nameof(Walk);

    [SerializeField] private float _speedMove;
    [SerializeField] private float _speedLook;

    private Transform _baseTransform;
    private Animator _animator;
    private Worker _worker;
    private Getter _getter;
    private Transform _targetTransform;

    private void Awake()
    {
        _worker = GetComponent<Worker>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _getter = GetComponent<Getter>();
        _baseTransform = transform.parent.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _worker.TargetReceived += SetTarget;
    }

    private void OnDisable()
    {
        _worker.TargetReceived -= SetTarget;
    }

    private void Move()
    {
        if (_targetTransform == null)
            return;

        Transform target;

        if (_targetTransform != null && _getter.PickingObject == null)
        {
            target = _targetTransform;
        }
        else
        {
            target = _baseTransform;
        }

        _animator.SetBool(Walk, true);
        transform.position = Vector3.MoveTowards(transform.position,
               target.position, _speedMove * Time.deltaTime);

        Look(target);
    }

    private void Look(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation,_speedLook * Time.deltaTime);
    }

    private void SetTarget(Transform target)
    {
        _targetTransform = target;
    }

    private void Update()
    {
        if (_worker.IsWork == false)
            return;

        Move();
    }

    public void SetTransformBase(Transform transformTarget)
    {
        _baseTransform = transformTarget;
    }

    public void Reset()
    {
        _animator.SetBool(Walk, false);
        _targetTransform = null;
    }
}

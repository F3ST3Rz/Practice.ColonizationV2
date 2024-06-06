using UnityEngine;
using UnityEngine.InputSystem.HID;

[RequireComponent (typeof(Worker))]
public class Getter : MonoBehaviour
{
    [SerializeField] private float _pickDistance;
    [SerializeField] private float _putDistance;
    [SerializeField] private float _holdDistance;
    [SerializeField] private Transform _pointRay;

    private Worker _worker;
 
    private PickingObject _pickingObject;

    public PickingObject PickingObject => _pickingObject;

    private void Start()
    {
        _worker = GetComponent<Worker> ();
    }

    private void Update()
    {
        if (_worker.IsWork == false)
            return;

        PickUp();
        PutUp();
    }

    private void PickUp()
    {
        if (_pickingObject != null)
            return;

        if (Physics.Raycast(_pointRay.position, transform.forward, out RaycastHit hitInfo, _pickDistance) == false)
            return;

        if (hitInfo.transform.TryGetComponent(out PickingObject pickingObject) == false)
            return;

        _pickingObject = pickingObject;
        _pickingObject.PickUp(transform, _holdDistance);
    }

    private void PutUp()
    {
        if (_pickingObject == null)
            return;

        if (Physics.Raycast(_pointRay.position, transform.forward, out RaycastHit hitInfo, _putDistance) == false)
            return;

        if (hitInfo.transform.TryGetComponent(out Storager storager) == false)
            return;

        storager.PutUp(_worker, _pickingObject.GetComponent<Box>());
    }

    public void Reset()
    {
        _pickingObject = null;
    }
}

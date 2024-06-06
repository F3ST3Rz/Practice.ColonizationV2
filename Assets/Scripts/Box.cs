using UnityEngine;

[RequireComponent (typeof(PickingObject))]
public class Box : MonoBehaviour
{
    [SerializeField] private PickingObject _pickingObject;

    private bool _isDetected = false;

    public bool IsDetected => _isDetected;

    public void SetDetected()
    {
        _isDetected = true;
    }

    public void Reset()
    {
        _isDetected = false;
        _pickingObject.Reset();
    }
}

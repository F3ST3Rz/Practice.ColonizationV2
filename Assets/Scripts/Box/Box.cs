using UnityEngine;

[RequireComponent (typeof(PickingObject))]
public class Box : MonoBehaviour
{
    private PickingObject _pickingObject;

    private void Start()
    {
        _pickingObject = GetComponent<PickingObject>();
    }

    public void Reset()
    {
        _pickingObject.Reset();
    }
}

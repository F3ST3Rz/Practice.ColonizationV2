using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PickingObject : MonoBehaviour
{
    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void PickUp(Transform parent, float holdDistance)
    {
        transform.SetParent(parent);
        transform.localPosition = new Vector3(0f, 0.5f, holdDistance);
        _rigidBody.isKinematic = true;
    }

    public void Reset()
    {
        _rigidBody.isKinematic = false;
    }
}

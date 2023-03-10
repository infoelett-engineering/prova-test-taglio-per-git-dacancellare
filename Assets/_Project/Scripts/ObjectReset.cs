using UnityEngine;

public class ObjectReset : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    
    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }

    public void ResetObject()
    {
        transform.SetPositionAndRotation(_initialPosition, _initialRotation);

        Rigidbody rb = transform.GetComponentInParent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.ResetInertiaTensor();
    }
}
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class SinglePieceSimulator : MonoBehaviour
{
    [SerializeField] private GameObject _objectToActivate;
    private Transform _initialParent;
    private Vector3 _localPosition;
    private Vector3 _localRotation;

    // Start is called before the first frame update
    private void Start()
    {
        _initialParent = transform.parent;
        _localPosition = transform.localPosition;
        _localRotation = transform.localRotation.eulerAngles;
    }

    public void ResetObject()
    {
        if(_objectToActivate != null)
            _objectToActivate.SetActive(false);
        
        transform.SetParent(_initialParent);
        transform.SetLocalPositionAndRotation(_localPosition, Quaternion.Euler(_localRotation));
        
        Destroy(gameObject.GetComponent<Rigidbody>());
    }

    public void ActivateGravity()
    {
        transform.SetParent(null);

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.AddForce(transform.TransformDirection(new Vector3(0f, 1f, -1f)), ForceMode.Impulse);
    }

    public void DisableObject()
    {
        if(_objectToActivate != null)
            _objectToActivate.SetActive(true);
        
        gameObject.SetActive(false);
    }
}
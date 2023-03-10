using Cysharp.Threading.Tasks;
using System;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

public class SinglePiece : MonoBehaviour
{
    [SerializeField] private int _pieceIndex;
    [SerializeField] private bool _swapObject;
    [SerializeField] private GameObject _objectToActivate;

    private bool _isSliced = false;
    
    private void Awake()
    {
        _isSliced = false;
    }

    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slicer") && _swapObject && _isSliced == false)
        {
            // Disabilito momentaneamente il collider della lama
            other.enabled = false;
            
            // Disabilito il renderer del seguente pezzo (dato che non ha fisica devo disabilitare solo il renderer e non tutto l'oggetto)
            GetComponent<Renderer>().enabled = false;

            HVRHandGrabber hand = (HVRHandGrabber)other.GetComponentInParent<HVRGrabbable>().PrimaryGrabber;
            hand.Controller.Vibrate(amplitude:200f, duration: 0.25f, frequency: 150f);

            _isSliced = true;
            
            if(_objectToActivate != null)
            {
                _objectToActivate.SetActive(true);
            }

            _isSliced = true;
            StepManager stepManager = GetComponentInParent<StepManager>();
            stepManager.NextPiece(_pieceIndex);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            // Riattivo il collider della lama
            other.enabled = true;

            // Ora che ho completato tutte le operazioni, posso anche disattivare l'oggetto stesso
            gameObject.SetActive(false);
        }
        else
        {
            if (other.CompareTag("Slicer") && _isSliced == false)
            {
                // Disabilito momentaneamente il collider della lama
                other.enabled = false;

                HVRHandGrabber hand =  (HVRHandGrabber)other.GetComponentInParent<HVRGrabbable>().PrimaryGrabber;
                hand.Controller.Vibrate(amplitude:200f, duration: 0.25f, frequency: 150f);

                _isSliced = true;
                StepManager stepManager = GetComponentInParent<StepManager>();
                stepManager.NextPiece(_pieceIndex);

                transform.SetParent(null);

                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.AddForce(transform.TransformDirection(new Vector3(0f, 1f, -1f)), ForceMode.Impulse);

                await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

                GetComponent<Collider>().isTrigger = false;

                GetComponent<Renderer>().sharedMaterial = stepManager.NormalMaterial;

                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

                // Riattivo il collider della lama
                other.enabled = true;
            }
        }
    }
}
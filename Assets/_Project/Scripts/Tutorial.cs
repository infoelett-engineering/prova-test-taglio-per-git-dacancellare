using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private HVRGrabbable _objectGrab;
    [SerializeField] private HVRGrabbable _sliderGrab;

    private bool _objectGrabbed = false;
    private bool _slicerGrabbed = false;

    private void Awake()
    {
        _objectGrabbed = false;
        _slicerGrabbed = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _objectGrab.HandGrabbed.AddListener(OnGrab);
        _sliderGrab.HandGrabbed.AddListener(OnGrab);
    }

    private void OnGrab(HVRHandGrabber grabber, HVRGrabbable grabbable)
    {
        if(grabbable.name == "GrabObject")
        {
            _objectGrabbed = true;
            grabbable.HandGrabbed.RemoveListener(OnGrab);
        }
        else if(grabbable.name == "Knife_Grabbable")
        {
            _slicerGrabbed = true;
            grabbable.HandGrabbed.RemoveListener(OnGrab);
        }

        CheckEndTutorial();
    }

    private void CheckEndTutorial()
    {
        if (_objectGrabbed && _slicerGrabbed)
        {
            //_tvManager.ChangeTextPanelUI("Procedi: Incidi");
            TVManager.Instance.ActivatePanelVideo();
            TVManager.Instance.PlayVideo(0);
        }
    }
}
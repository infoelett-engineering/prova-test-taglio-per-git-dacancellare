using Cysharp.Threading.Tasks;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Core.Grabbers;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HolsterConfirm : HVRSocket
{
    private StepManager _stepManager;
    private List<GameObject> _steps = new();

    protected override void Start()
    {
        base.Start();

        Grabbed.AddListener(OnGrabbed);
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _stepManager = FindObjectOfType<StepManager>();

        _steps.Clear();
        foreach (Transform child in transform)
        {
            _steps.Add(child.gameObject);
        }

        if (_stepManager.CurrentStepPiece + 1 < _stepManager.MaxStepPiece)
        {
            _steps[_stepManager.CurrentStepPiece].SetActive(true);
        }
    }

    private async void OnGrabbed(HVRGrabberBase grabber, HVRGrabbable grabbable)
    {        
        ForceRelease();

        _steps[_stepManager.CurrentStepPiece].SetActive(false);

        _stepManager.NextStep();

        Destroy(GetComponent<HVRTriggerGrabbableBag>());

        gameObject.AddComponent<HolsterConfirm>();

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        Destroy(this);

        gameObject.SetActive(false);

        Debug.Log("OnBeforeGrabbed Invoke");
    }
}
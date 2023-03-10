using BzKovSoft.ObjectSlicer;
using UnityEngine;
using System;
using Plane = UnityEngine.Plane;
using HurricaneVR.Framework.Core.Grabbers;
using System.Collections.Generic;
using BzKovSoft.ObjectSlicer.EventHandlers;
using BzKovSoft.ObjectSlicer.Samples;

public class ObjectToSlice : BzSliceableObjectBase, IBzObjectSlicedEvent, IBzSliceableNoRepeat
{
    [HideInInspector, SerializeField] int _sliceId;
    [HideInInspector, SerializeField] float _lastSliceTime = float.MinValue;
    
    public float delayBetweenSlices = 1f;

    public void Slice(Plane plane, int sliceId, Action<BzSliceTryResult> callBack)
    {
        float currentSliceTime = Time.time;

        // we should prevent slicing same object:
        // - if _delayBetweenSlices was not exceeded
        // - with the same sliceId
        if ((sliceId == 0 & _lastSliceTime + delayBetweenSlices > currentSliceTime) |
            (sliceId != 0 & _sliceId == sliceId))
        {
            return;
        }

        // exit if it have LazyActionRunner
        if (GetComponent<LazyActionRunner>() != null)
            return;

        _lastSliceTime = currentSliceTime;
        _sliceId = sliceId;

        Slice(plane, callBack);
    }

    protected override BzSliceTryData PrepareData(Plane plane)
    {
        OriginalData data = new OriginalData();
        data.Grabbers = gameObject.GetComponentInChildren<HVRGrabbableSlice>().Grabbers;

        // colliders that will be participating in slicing
        var colliders = gameObject.GetComponentsInChildren<Collider>();

        // return data
        return new BzSliceTryData()
        {
            // componentManager: this class will manage components on sliced objects
            componentManager = new StaticComponentManager(gameObject, plane, colliders),
            plane = plane,
            addData= data
        };
    }

    public void ObjectSliced(GameObject original, GameObject resutlNeg, GameObject resultPos)
    {
        if (resultPos.GetComponent<HVRGrabbableSlice>() == null)
            return;

        resultPos.name = "SliceOne";
        resutlNeg.name = "SliceTwo";

        if(resultPos.transform.Find("Physics LeftHand GrabPoint") != null)
            Destroy(resultPos.transform.Find("Physics LeftHand GrabPoint").gameObject);

        if(resultPos.transform.Find("Physics RightHand GrabPoint") != null)
            Destroy(resultPos.transform.Find("Physics RightHand GrabPoint").gameObject);

        if (resutlNeg.transform.Find("Physics LeftHand GrabPoint") != null)
            Destroy(resutlNeg.transform.Find("Physics LeftHand GrabPoint").gameObject);

        if (resutlNeg.transform.Find("Physics RightHand GrabPoint") != null)
            Destroy(resutlNeg.transform.Find("Physics RightHand GrabPoint").gameObject);

        resultPos.GetComponent<HVRGrabbableSlice>().ReInitiliaze();
        resutlNeg.GetComponent<HVRGrabbableSlice>().ReInitiliaze();

        if (resultPos.GetComponent<Joint>() != null)
        {
            Destroy(resultPos.GetComponent<Joint>());
            Destroy(resutlNeg.GetComponent<Joint>());

            //if (addData.Grabbers.Count > 0)
            //{
            //    if (resutlNeg.GetComponentInChildren<Mesh>().vertexCount < resultPos.GetComponentInChildren<Mesh>().vertexCount)
            //    {
            //        addData.Grabbers[0].TryGrab(resultPos.GetComponent<HVRGrabbableSlice>(), true);
            //    }
            //    else
            //    {
            //        addData.Grabbers[0].TryGrab(resutlNeg.GetComponent<HVRGrabbableSlice>(), true);
            //    }
            //}
        }
    }

    public class OriginalData 
    {
        public List<HVRGrabberBase> Grabbers;
    }
}
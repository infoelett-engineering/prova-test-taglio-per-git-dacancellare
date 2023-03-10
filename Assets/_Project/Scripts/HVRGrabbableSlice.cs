using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using UnityEngine;

public class HVRGrabbableSlice : HVRGrabbable
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public void ReInitiliaze()
    {
        base.Awake();
        base.Start();

        Debug.Log("Re-initialize object", gameObject);
    }

    protected override void OnGrabbed(HVRGrabberBase grabber)
    {
        for (int x = 0; x < Colliders.Count; x++)
        {
            if (Colliders[x] == null)
                Colliders.RemoveAt(x);
        }

        base.OnGrabbed(grabber);
    }

    protected override void OnHoverEnter(HVRGrabberBase grabber)
    {
        for (int x = 0; x < Colliders.Count; x++)
        {
            if (Colliders[x] == null)
                Colliders.RemoveAt(x);
        }

        base.OnHoverEnter(grabber);
    }
}

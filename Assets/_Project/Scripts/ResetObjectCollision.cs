using UnityEngine;

public class ResetObjectCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Grabbable"))
        {
            col.collider.GetComponentInParent<ObjectReset>()?.ResetObject();
        }
    }
}
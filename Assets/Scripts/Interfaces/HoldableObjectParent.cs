using UnityEngine;

public interface HoldableObjectParent
{
    public Transform GetHoldableObjectFollowTransform();

    public void SetHoldableObject(HoldableObject holdableObject);

    public HoldableObject GetHoldableObject();

    public void ClearHoldableObject();

    public bool HasHoldableObject();
}

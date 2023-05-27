using UnityEngine;

public interface ICollectable
{
    public void FlyToTargetAndDisappear(Vector3 targetPos);
    public void FlyToPosAsChildren(Vector3 localPos, Vector3 eulersToRotate);
    public void SetParent(Transform parent);
    public bool CanCollect();
    public ItemTypes GetItemType();
}
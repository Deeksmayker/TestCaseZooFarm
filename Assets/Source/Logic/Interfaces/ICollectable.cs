using UnityEngine;

public interface ICollectable
{
    public void FlyToTargetAndDisappear(Transform target);
    public void FlyToPosAsChildren(Vector3 localPos);
    public void SetParent(Transform parent);
    public bool CanCollect();
    public ItemTypes GetItemType();
}
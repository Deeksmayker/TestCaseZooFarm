using UnityEngine;

public interface ICollectable
{
    public void Collect(Transform target);
    public bool CanCollect();
}
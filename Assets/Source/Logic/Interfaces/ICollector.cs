using System;

public interface ICollector
{
    public event Action OnCollected;

    public void CollectOnRadius();
    public bool CanCollect();
}
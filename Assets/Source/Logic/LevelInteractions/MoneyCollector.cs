using System;
using UnityEngine;

public class MoneyCollector : MonoBehaviour, ICollector
{
    [SerializeField] private LayerMask layersToCollect;
    [SerializeField] private float radius;

    private readonly Collider[] _targets = new Collider[20];

    public event Action OnCollected;
    public event Action<int> OnMoneyCollected;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        CollectOnRadius();
    }

    public void CollectOnRadius()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, _targets, layersToCollect);

        for (var i = 0; i < _targets.Length; i++)
        {
            if (!_targets[i])
                break;
            if (_targets[i].TryGetComponent<MoneyCollectable>(out var money) && money.CanCollect())
            {
                money.FlyToTargetAndDisappear(_transform);
                OnCollected?.Invoke();
                OnMoneyCollected?.Invoke(money.GetMoneyAmount());
            }
        }
    }

    public bool CanCollect()
    {
        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
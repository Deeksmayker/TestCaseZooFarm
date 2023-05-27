using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Seedbed : MonoBehaviour
{
    [SerializeField] private BaseVegetable vegetablePrefab;
    [SerializeField] private bool activated;

    private BaseVegetable _connectedVegetable;

    private void Awake()
    {
        _connectedVegetable = GetComponentInChildren<BaseVegetable>();

        if (_connectedVegetable == null && activated)
        {
            SpawnVegetable();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activated || !_connectedVegetable.CanCollect())
            return;
        if (other.TryGetComponent<ItemsCarrier>(out var carrier))
        {
            if (!carrier.CanTakeItem())
                return;

            carrier.TakeNewItem(_connectedVegetable);

            _connectedVegetable = Instantiate(vegetablePrefab, transform);
            _connectedVegetable.transform.localPosition = Vector3.zero;
        }
    }

    public void ActivateSeedbed()
    {
        activated = true;
        SpawnVegetable();
    }

    private void SpawnVegetable()
    {
        _connectedVegetable = Instantiate(vegetablePrefab, transform);
        _connectedVegetable.transform.localPosition = Vector3.zero;
    }
}
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Seedbed : MonoBehaviour
{
    [SerializeField] private BaseVegetable vegetablePrefab;

    private BaseVegetable _connectedVegetable;

    private void Awake()
    {
        _connectedVegetable = GetComponentInChildren<BaseVegetable>();

        if (_connectedVegetable == null)
        {
            _connectedVegetable = Instantiate(vegetablePrefab, transform);
            _connectedVegetable.transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if (!_connectedVegetable.CanCollect())
            return;
        if (other.TryGetComponent<ItemsCarrier>(out var carrier))
        {
            Debug.Log(2);
            if (!carrier.CanTakeItem())
                return;

            Debug.Log(3);
            carrier.TakeNewItem(_connectedVegetable);

            _connectedVegetable = Instantiate(vegetablePrefab, transform);
            _connectedVegetable.transform.localPosition = Vector3.zero;
        }
    }
}
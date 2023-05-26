using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Aviary : MonoBehaviour
{
    [SerializeField] private Transform leftUpPoint, rightDownPoint;

    private ItemsRequester[] _requestersInside;

    private void Start()
    {
        _requestersInside = GetComponentsInChildren<ItemsRequester>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ItemsCarrier>(out var carrier))
        {
            for (var i = 0; i < _requestersInside.Length; i++)
            {
                if (!_requestersInside[i].NeedItem())
                    continue;
                var givenCount = carrier.GiveItemsOfTypeToTarget(_requestersInside[i].GetItemTypeRequested(), _requestersInside[i].transform, _requestersInside[i].GetNeedItemCount());
                for (var j = 0; j < givenCount; j++)
                    _requestersInside[i].TakeItem();
            }
        }
    }

    public Vector3 GetRandomPointInside()
    {
        return new Vector3(Random.Range(leftUpPoint.position.x, rightDownPoint.position.x),
            0,
            Random.Range(leftUpPoint.position.z, rightDownPoint.position.z));
    }
}
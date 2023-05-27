using System.Collections.Generic;
using UnityEngine;

public class ItemsCarrier : MonoBehaviour
{
    [SerializeField] private int maxItems = 2;
    [SerializeField] private Transform itemHoldPoint;

    private int _holdingItemsCount;

    private List<ICollectable> _currentItems = new();

    public void TakeNewItem(ICollectable newItem)
    {
        var localHoldPoint = itemHoldPoint.localPosition + Vector3.up * _holdingItemsCount / 4;

        newItem.SetParent(transform);
        newItem.FlyToPosAsChildren(localHoldPoint, new Vector3(90, 0, 90));
        _holdingItemsCount++;

        _currentItems.Add(newItem);
    }

    public int GiveItemsOfTypeToTarget(ItemTypes itemType, Transform target, int count)
    {
        var givenCount = 0;
        for (var i = _currentItems.Count - 1; i >= 0; i--)
        {
            if (givenCount >= count)
                return givenCount;

            if (_currentItems[i] == null || _currentItems[i].GetItemType() != itemType)
                continue;

            _currentItems[i].SetParent(target);
            _currentItems[i].FlyToTargetAndDisappear(target.position);
            _holdingItemsCount--;
            _currentItems.RemoveAt(i);
            givenCount++;
        }

        return givenCount;
    }

    public bool CanTakeItem() => _holdingItemsCount < maxItems;
}
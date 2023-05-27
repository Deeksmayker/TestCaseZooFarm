using System;
using UnityEngine;

public class ItemsRequester : MonoBehaviour
{
	[SerializeField] private ItemTypes itemTypeRequested;
	[SerializeField] private int needItemCount;

	private int _itemsCollected;

	public event Action<int> OnItemCollected;
	public event Action OnAllItemsCollected;

	public void TakeItem()
	{
		_itemsCollected++;
		OnItemCollected?.Invoke(_itemsCollected);

		if (_itemsCollected >= needItemCount)
		{
			OnAllItemsCollected?.Invoke();
		}
	}

	public ItemTypes GetItemTypeRequested() => itemTypeRequested;
	public int GetCurrentNeedItemCount() => needItemCount - _itemsCollected;
	public int GetMaxItemRequested() => needItemCount;
	public int GetCollectedCount() => _itemsCollected;
	public bool NeedItem() => _itemsCollected < needItemCount;
}
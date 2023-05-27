using TMPro;
using UnityEngine;

public class ItemsRequesterUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI requestedTextMesh;

    private ItemsRequester _requester;

    private void Awake()
    {
        _requester = GetComponentInParent<ItemsRequester>();
        HandleItemCollected(0);
    }

    private void OnEnable()
    {
        _requester.OnItemCollected += HandleItemCollected;
        _requester.OnAllItemsCollected += HandleAllItemsCollected;
    }

    private void OnDisable()
    {
        _requester.OnItemCollected -= HandleItemCollected;
        _requester.OnAllItemsCollected -= HandleAllItemsCollected;
    }

    private void HandleItemCollected(int newValue)
    {
        requestedTextMesh.text = $"{newValue}/{_requester.GetMaxItemRequested()}";
    }

    private void HandleAllItemsCollected()
    {
        requestedTextMesh.text = "MAX";
    }
}
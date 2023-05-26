using TMPro;
using UnityEngine;

public class BuyableCellVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceTextMesh;

    private BuyableCell _cell;

    private void Awake()
    {
        _cell = GetComponent<BuyableCell>();
        _priceTextMesh.text = _cell.GetPrice().ToString();
    }

    private void OnEnable()
    {
        _cell.OnPriceChanged += HandlePriceChanged;
    }

    private void OnDisable()
    {
        _cell.OnPriceChanged -= HandlePriceChanged;
    }

    private void HandlePriceChanged(int newPrice)
    {
        _priceTextMesh.text = newPrice.ToString();
    }
}
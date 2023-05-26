using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BuyableCell : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private float moneySpendDelay = 0.3f;
    [SerializeField] private float spendMoneyTime = 1f;

    private float _delayTimer;
    private float _t;

    private bool _targetInArea;

    private PlayerMoneyHandler _moneyHandler;

    public event Action OnEnterArea;
    public event Action OnExitArea;
    public event Action<int> OnPriceChanged;
    public event Action OnFullPricePaid;

    private void Update()
    {
        if (!_targetInArea)
            return;

        if (_delayTimer > 0)
        {
            _delayTimer -= Time.deltaTime;
            return;
        }

        SetNewPrice(Mathf.CeilToInt(Mathf.Lerp(price, 0, _t)));

        _t += Time.deltaTime / spendMoneyTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _moneyHandler))
        {
            _targetInArea = true;
            _delayTimer = moneySpendDelay;
            OnEnterArea?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMoneyHandler>())
        {
            _moneyHandler = null;
            _targetInArea = false;
            OnExitArea?.Invoke();
        }
    }

    public int GetPrice()
    {
        return price;
    }

    private void SetNewPrice(int newPrice)
    {
        if (newPrice == price)
            return;
        Debug.Log(newPrice);
        _moneyHandler.RemoveMoney(price - newPrice);
        price = newPrice;
        OnPriceChanged?.Invoke(price);

        if (price <= 0)
        {
            OnFullPricePaid?.Invoke();
            Destroy(gameObject);
        }
    }
}
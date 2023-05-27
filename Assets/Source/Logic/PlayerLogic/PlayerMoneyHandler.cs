using System;
using UnityEngine;

public class PlayerMoneyHandler : MonoBehaviour
{
    [SerializeField] private MoneyCollectable moneyPrefab;
    [SerializeField] private int moneyAmount;

    private MoneyCollector _moneyCollector;

    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        _moneyCollector = GetComponent<MoneyCollector>();
    }

    private void OnEnable()
    {
        if (_moneyCollector)
        {
            _moneyCollector.OnMoneyCollected += AddMoney;
        }
    }

    private void OnDisable()
    {
        if (_moneyCollector)
        {
            _moneyCollector.OnMoneyCollected -= AddMoney;
        }
    }

    public void AddMoney(int addedAmount)
    {
        moneyAmount += addedAmount;
        OnMoneyChanged?.Invoke(moneyAmount);
    }

    public void RemoveMoney(int removedAmount)
    {
        moneyAmount -= removedAmount;
        OnMoneyChanged?.Invoke(moneyAmount);
    }

    public void MoveMoneyMeshToTarget(Vector3 targetPos)
    {
        //TODO: Сделать через обжект пулинг
        var money = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
        money.FlyToTargetAndDisappear(targetPos);
    }

    public int GetMoneyAmount() => moneyAmount;
}
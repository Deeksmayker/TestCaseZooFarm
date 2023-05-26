using UnityEngine;

public class MoneyCollectable : BaseCollectable
{
    [SerializeField] private int moneyAmount = 5;

    public int GetMoneyAmount()
    {
        return moneyAmount;
    }

    public override ItemTypes GetItemType()
    {
        return ItemTypes.Money;
    }
}
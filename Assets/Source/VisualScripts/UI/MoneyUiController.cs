using TMPro;
using UnityEngine;

public class MoneyUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private PlayerMoneyHandler _moneyHandler;

    private void Awake()
    {
        _moneyHandler = FindObjectOfType<PlayerMoneyHandler>();

        if (!_moneyHandler)
        {
            Debug.LogWarning("No money handler on scene");
        }
    }

    private void OnEnable()
    {
        _moneyHandler.OnMoneyChanged += HandleMoneyChanged;
    }

    private void OnDisable()
    {
        _moneyHandler.OnMoneyChanged -= HandleMoneyChanged;
    }

    private void HandleMoneyChanged(int newValue)
    {
        textMesh.text = newValue.ToString();
    }
}
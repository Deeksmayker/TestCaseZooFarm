using TMPro;
using UnityEngine;

public class MoneyUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private float _currentMoney;
    private float _visibleMoney;

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

    private void Update()
    {
        if (_currentMoney.Equals(_visibleMoney))
            return;

        _visibleMoney = Mathf.Lerp(_visibleMoney, _currentMoney, Time.deltaTime * 3);
        textMesh.text = Mathf.RoundToInt(_visibleMoney).ToString();
    }

    private void HandleMoneyChanged(int newValue)
    {
        _currentMoney = newValue;
    }
}
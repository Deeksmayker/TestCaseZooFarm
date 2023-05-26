using UnityEngine;

public class MoneyCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private int moneyAmount = 5;
    [SerializeField] private float flyTime;

    private bool _targetFound;
    private Transform _targetTransform;
    private float _t;

    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (!_targetFound)
            return;

        transform.position = Vector3.Lerp(_startPosition, _targetTransform.position, _t);
        _t += Time.deltaTime / flyTime;

        if (_t >= 1f)
        {
            Destroy(gameObject);
        }
    }

    public void Collect(Transform target)
    {
        _targetTransform = target;
        _targetFound = true;
    }

    public bool CanCollect()
    {
        return !_targetFound;
    }

    public int GetMoneyAmount()
    {
        return moneyAmount;
    }
}
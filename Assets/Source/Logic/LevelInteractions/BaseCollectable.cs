using UnityEngine;

public class BaseCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private float flyTime;

    protected bool _flyingToTarget;
    protected bool _flyingToNewLocalPos;

    protected float _flyProgress;

    protected Vector3 _targetPosition;
    protected Vector3 _targetEulersToRotate;
    protected Vector3 _startPosition;
    protected Vector3 _startScale;
    protected Vector3 _startEulers;

    private Vector3 _newLocalPosition;

    protected virtual void Awake()
    {
        _startPosition = transform.position;
        _startScale = transform.localScale;
        _startEulers = transform.eulerAngles;
    }

    protected virtual void Update()
    {
        if (_flyingToTarget)
        {

            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _flyProgress);
            transform.localScale = Vector3.Lerp(_startScale, Vector3.zero, _flyProgress);
            _flyProgress += Time.deltaTime / flyTime;

            if (_flyProgress >= 1f)
            {
                Destroy(gameObject);
            }
            return;
        }
        
        if (_flyingToNewLocalPos)
        {
            transform.localPosition = Vector3.Lerp(_startPosition, _newLocalPosition, _flyProgress);
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(_startEulers), Quaternion.Euler(_targetEulersToRotate), _flyProgress);
            _flyProgress += Time.deltaTime / flyTime;

            if (_flyProgress >= 1f)
            {
                _flyingToNewLocalPos = false;
                _flyProgress = 0f;
            }
        }
    }

    public void FlyToTargetAndDisappear(Vector3 targetPos)
    {
        _startPosition = transform.position;
        _targetPosition = targetPos;
        _flyingToTarget = true;
    }

    public void FlyToPosAsChildren(Vector3 localPos, Vector3 eulersToRotate)
    {
        _startPosition = transform.localPosition;
        _flyingToNewLocalPos = true;
        _newLocalPosition = localPos;
        _targetEulersToRotate = eulersToRotate;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    public virtual bool CanCollect()
    {
        return !_flyingToTarget;
    }

    public virtual ItemTypes GetItemType() => ItemTypes.None;
}
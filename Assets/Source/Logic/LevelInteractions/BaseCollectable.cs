using UnityEngine;

public class BaseCollectable : MonoBehaviour, ICollectable
{
    [SerializeField] private float flyTime;

    protected Transform _targetTransform;

    protected bool _flyingToTarget;
    protected bool _flyingToNewLocalPos;

    protected float _flyProgress;

    protected Vector3 _startPosition;

    private Vector3 _newLocalPosition;

    protected virtual void Awake()
    {
        _startPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (_flyingToTarget)
        {
            transform.position = Vector3.Lerp(_startPosition, _targetTransform.position, _flyProgress);
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
            _flyProgress += Time.deltaTime / flyTime;

            if (_flyProgress >= 1f)
            {
                _flyingToNewLocalPos = false;
                _flyProgress = 0f;
            }
        }
    }

    public void FlyToTargetAndDisappear(Transform target)
    {
        _startPosition = transform.position;
        _targetTransform = target;
        _flyingToTarget = true;
    }

    public void FlyToPosAsChildren(Vector3 localPos)
    {
        _startPosition = transform.position;
        _flyingToNewLocalPos = true;
        _newLocalPosition = localPos;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    private void FlyToTarget(bool changeLocalPosition, Vector3 newPos, bool destroyAfterReach)
    {
        
    }

    public virtual bool CanCollect()
    {
        return !_flyingToTarget;
    }

    public virtual ItemTypes GetItemType() => ItemTypes.None;
}
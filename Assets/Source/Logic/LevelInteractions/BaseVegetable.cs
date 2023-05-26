using System;
using UnityEngine;

public class BaseVegetable : BaseCollectable, IGrowingUp
{
    [SerializeField] private ItemTypes itemType;
    [SerializeField] private float growTime = 5f;
    [SerializeField] private Mesh grownCarrotMesh;

    private bool _grewUp;

    private float _growTimer;

    private MeshFilter _meshFilter;

    public event Action OnGrewUp;

    protected override void Awake()
    {
        base.Awake();
        _meshFilter = GetComponentInChildren<MeshFilter>();
    }

    protected override void Update()
    {
        base.Update();

        _growTimer += Time.deltaTime;

        if (!_grewUp && _growTimer >= growTime)
        {
            _meshFilter.mesh = grownCarrotMesh;
            transform.Rotate(90, 0, 0);
            _grewUp = true;
            OnGrewUp?.Invoke();
        }
    }

    public override bool CanCollect()
    {
        return !_flyingToNewLocalPos && _grewUp;
    }

    public override ItemTypes GetItemType()
    {
        return itemType;
    }
}
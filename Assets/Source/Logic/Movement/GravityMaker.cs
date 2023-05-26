using UnityEngine;


public class GravityMaker : MonoBehaviour
{
    /*public float RiseGravity = -50;

    public float FallingGravity = -30;

    [SerializeField] private float changingGravityVelocity = 10;*/
    //public Vector3 GravityDirection = Vector3.down;

    private IMover _mover;

    private void Awake()
    {
        _mover = GetComponent<IMover>();
    }

    private void Update()
    {
        _mover.SetVerticalVelocity(-5);
    }
}

using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private DefaultMover _mover;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _mover = GetComponentInParent<DefaultMover>();
    }

    private void Update()
    {
        if (_mover.GetVelocityMagnitude() > 0.1f) 
        {
            _animator.SetBool("Moving", true);
        }
        else
        {
            _animator.SetBool("Moving", false);
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerUnit))]
public class PlayerInputHandler : MonoBehaviour
{
    private float _jumpInputTimer;
    
    private IMover _mover;
    
    private PlayerInput _playerInput;

    private void Awake()
    {
        _mover = GetComponent<DefaultMover>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_mover != null)
        {
            _mover.SetHorizontalInput(_playerInput.actions[InputConsts.Move].ReadValue<Vector2>());
        }
    }
}



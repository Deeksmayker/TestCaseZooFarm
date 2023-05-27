using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.OnScreen;

public class ScreenJoystickUiController : MonoBehaviour
{
    [SerializeField] private GameObject joystickImage;
    private PlayerInput _input;

    private void Start()
    {
        _input = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        Debug.Log(_input.actions[InputConsts.Touch].IsInProgress());
        /*if (_input.actions[InputConsts.Touch].IsPressed())
        {
            joystickImage.SetActive(true);
           
        }
        else if (joystickImage.activeSelf)
        {
            joystickImage.SetActive(false);
        }*/
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputs _playerInput;
    Vector2 _moveInput;
    PlayerMovementComponent _playerMovementComp;
    PlayerGunComponent _playerGunComponent;

    private void OnEnable()
    {
        _playerInput.Enable();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Awake()
    {
        _playerInput = new PlayerInputs();
    }
    private void Start()
    {
        _playerMovementComp = GetComponent<PlayerMovementComponent>();
        _playerGunComponent = GetComponent<PlayerGunComponent>();
        SetUpPlayerInput();
    }

    private void SetUpPlayerInput()
    {
        _playerInput.Gameplay.Move.performed += OnMoveUpdated;
        _playerInput.Gameplay.Move.canceled += OnMoveUpdated;

        _playerInput.Gameplay.Boost.performed += OnBoostActive;
        _playerInput.Gameplay.Boost.canceled += OnBoostDeactive;

        _playerInput.Gameplay.Shoot.performed += OnShootPressed;
    }

    private void OnShootPressed(InputAction.CallbackContext obj)
    {
        _playerGunComponent.Fire();
    }

    private void OnBoostActive(InputAction.CallbackContext obj)
    {
        _playerMovementComp.BoostActive();
    }
    private void OnBoostDeactive(InputAction.CallbackContext obj)
    {
        _playerMovementComp.BoostDeactive();
    }

    private void OnMoveUpdated(InputAction.CallbackContext obj)
    {
        _moveInput = obj.ReadValue<Vector2>();
        _playerMovementComp.UpdatedMoveInput(_moveInput);
    }

    public void DisablePlayerControls()
    {
        _playerInput.Gameplay.Disable();
    }
}

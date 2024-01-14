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
    [SerializeField] AudioSource HealthSound;
    [SerializeField] AudioSource BoostUpSound;
    [SerializeField] AudioSource BombPickUpSound;

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
        _playerGunComponent = GetComponent<PlayerGunComponent>();;
        SetUpPlayerInput();
    }

    private void SetUpPlayerInput()
    {
        _playerInput.Gameplay.Move.performed += OnMoveUpdated;
        _playerInput.Gameplay.Move.canceled += OnMoveUpdated;

        _playerInput.Gameplay.Boost.performed += OnBoostActive;
        _playerInput.Gameplay.Boost.canceled += OnBoostDeactive;

        _playerInput.Gameplay.Shoot.performed += OnShootPressed;

        _playerInput.Gameplay.Exit.performed += OnExitPressed;

        _playerInput.Gameplay.Bomb.performed += OnBombPressed;
    }

    private void OnBombPressed(InputAction.CallbackContext obj)
    {
        _playerGunComponent.DropBomb();
    }

    private void OnExitPressed(InputAction.CallbackContext obj)
    {
        Application.Quit();
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

    // item sounds

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        HealthSound.Play();

        else if (other.CompareTag("BoostUp"))
            BoostUpSound.Play();

        else if (other.CompareTag("Bomb"))
            BombPickUpSound.Play();
        
    }
}

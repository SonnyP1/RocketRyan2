using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Player Inputs
    PlayerInputs _playerInput;
    Vector2 _moveInput;

    //Player Cart
    [SerializeField] Cart _cart;
    [SerializeField] Transform _cartSpawnPoint;

    //Player Components
    PlayerMovementComponent _playerMovementComp;
    PlayerGunComponent _playerGunComponent;

    //Player Audio
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
        GameObject scoreKeeper = new GameObject();
        scoreKeeper.name = "ScoreKeeper";
        scoreKeeper.transform.parent = null;
        scoreKeeper.AddComponent<ScoreKeeper>();
    }
    private void Start()
    {
        InitComponents();
        SetUpPlayerInput();
    }

    void InitComponents()
    {
        _playerMovementComp = GetComponent<PlayerMovementComponent>();
        _playerMovementComp.Speed = _cart.m_moveSpeed;
        _playerMovementComp.RotSpeed = _cart.m_turnSpeed;
        _playerMovementComp.BoostTransform = SpawnTransform().transform;
        _playerMovementComp.BoostTransform.localPosition = _cart.m_boostOffset;
        _playerMovementComp.BoostTransform.name = "Boost Spawn Point";

        _playerGunComponent = GetComponent<PlayerGunComponent>(); ;
        _playerGunComponent.ProjectilePrefab = _cart.m_projectileType;
        _playerGunComponent.BombPrefab = _cart.m_bombType;
        _playerGunComponent.BombMaxAmmo = _cart.m_maxBombs;
        _playerGunComponent.ProjectileSpawnPoint = SpawnTransform().transform;
        _playerGunComponent.ProjectileSpawnPoint.localPosition = _cart.m_projectileOffset;
        _playerGunComponent.ProjectileSpawnPoint.name = "Projectile Spawn Point";

        Instantiate(_cart.m_cartModel, _cartSpawnPoint);

    }

    private GameObject SpawnTransform()
    {
        GameObject obj = new GameObject();
        obj.transform.parent = _cartSpawnPoint;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localEulerAngles = Vector3.zero;
        return obj;
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

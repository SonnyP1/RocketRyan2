using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player Script manage player inputs and setup the player carts and stats from the scriptable object Cart class
/// e.g. MoveInput - Fire Inputs
/// NOTE: Prob should move audio to like a audio manager script or something
/// </summary>
public class Player : NetworkBehaviour
{
    [Header("Player Audio")]
    [SerializeField] AudioSource _healthSound;
    [SerializeField] AudioSource _boostUpSound;
    [SerializeField] AudioSource _bombPickUpSound;


    [Header("Player Stats")]
    [SerializeField] Cart _cart;
    [SerializeField] Transform _cartSpawnPoint;

    //Player Components
    private PlayerMovementComponent _playerMovementComp;
    private PlayerGunComponent _playerGunComponent;

    //Player Inputs
    private PlayerInputs _playerInput;
    private Vector2 _moveInput;


    #region Unity Functions
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void Awake()
    {
        _playerInput = new PlayerInputs();
        //Taking this out for right now because it instantly wants to jump to the next level
        
        /* 
        GameObject scoreKeeper = new GameObject();
        scoreKeeper.name = "ScoreKeeper";
        scoreKeeper.transform.parent = null;
        scoreKeeper.AddComponent<ScoreKeeper>();
        */
    }
    private void Start()
    {
        InitComponents();
        SetUpPlayerInput();
    }
    private void OnTriggerEnter(Collider other)
    {
        // item sounds
        if (other.CompareTag("Health"))
        _healthSound.Play();

        else if (other.CompareTag("BoostUp"))
            _boostUpSound.Play();

        else if (other.CompareTag("Bomb"))
            _bombPickUpSound.Play();
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }
    #endregion

    #region Custom Functions
    //Init Components such as Movement & PlayerGun
    private void InitComponents()
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
    #endregion

    #region Inputs Functions
    private void SetUpPlayerInput()
    {
        // changed the name of OnMoveUpdated to include the ServerRPC tag, it requires the method name to include it
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
        // might need to change this to a pausing mechanic rather than outright quitting the game
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


    // need to move the line for sending _moveInput to the movement component into a different function because we need a ServerRPC attributed function to handle the movement
    // server side
    private void OnMoveUpdated(InputAction.CallbackContext obj)
    {
        _moveInput = obj.ReadValue<Vector2>();
        UpdateMovementServerRPC(_moveInput);
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdateMovementServerRPC(Vector2 input)
    {

        _playerMovementComp.UpdatedMoveInput(input);
    }

    public void DisablePlayerControls()
    {
        _playerInput.Gameplay.Disable();
    }
    #endregion
}

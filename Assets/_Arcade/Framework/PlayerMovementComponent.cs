using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

/// <summary>
/// This script handles everything about the MOVEMENT of the player
/// e.g. Boost - Rotation - Speed
/// </summary>
public class PlayerMovementComponent : NetworkBehaviour
{
    [Header("Boost Serialize Fields")]
    [SerializeField] GameObject BoostEffect;
    [SerializeField] BoxCollider BoostCollider;
    [SerializeField] GameObject BoostFrontEffect;
    [SerializeField] AudioSource BoostSoundEffect;

    //Movement Variables
    [SerializeField]private float _speed = 5f;
    private float _rotspeed = 5f;

    //Boost Variables
    private float _boostCurrent = 0f;
    private float boostMax = 100f;
    private Transform _boostTransform;

    //Components Variables
    private CharacterController _characterController;
    private GameplayUIManager _gameplayUIManager;
    
    //Input Variables
    private Vector2 _moveInput;
    private bool _isBoostActive = false;

    #region Getters & Setters
    public float Speed
    {
        set { _speed = value; }
        get { return _speed;}
    }

    public float RotSpeed
    {
        set { _rotspeed = value; }
        get { return _rotspeed; }
    }
    internal float GetCurrentBoost()
    {
        return _boostCurrent;
    }

    public Transform BoostTransform
    {
        set { _boostTransform = value; }
        get { return _boostTransform; }
    }
    #endregion

    #region Unity Functions
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        BoostCollider.enabled = false;
        BoostFrontEffect.SetActive(false);
        if (scoreKeeper == null)
        {
            return;
        }

        if (scoreKeeper.GetCurrentBoostGlobal() > 0)
        {
            _boostCurrent = scoreKeeper.GetCurrentBoostGlobal();
        }
        else
        {
            _boostCurrent = boostMax;
        }
        UpdateBoostUI();
    }

    private void Update()
    {
        if (!IsOwner) return;
        UpdateRotation();
        if(_isBoostActive == true && _boostCurrent >= 0)
        {
            _boostCurrent -= 0.1f;
            _characterController.Move(-transform.right * (_speed*2) * Time.deltaTime);
            if(!BoostSoundEffect.isPlaying)
            {
                BoostSoundEffect.Play();
                BoostSoundEffect.loop = true;
            }
            GameObject newBoostEffect = Instantiate(BoostEffect,_boostTransform);
            newBoostEffect.transform.parent = null;
            UpdateBoostUI();
            BoostCollider.enabled = true;
            BoostFrontEffect.SetActive(true);
            return;
        }
        BoostSoundEffect.loop = false;
        BoostFrontEffect.SetActive(false);
        BoostCollider.enabled = false;
        _characterController.Move(-transform.right * _speed * Time.deltaTime);
    }
    #endregion

    #region Custom Functions
    public void ActivateBoostFrontEffect()
    {
        BoostFrontEffect.SetActive(true);
    }
    public void AddBoost(float value)
    {
        _boostCurrent = Mathf.Clamp(_boostCurrent + value,0,boostMax);
        UpdateBoostUI();
    }
    //UI
    private void UpdateBoostUI()
    {
        Debug.Log("Wants to update boost UI");
        //_gameplayUIManager.UpdateBoostSlider(_boostCurrent / boostMax);
    }
    #endregion

    #region Inputs Functions
    internal void UpdatedMoveInput(Vector2 moveInput)
    {
        _moveInput = new Vector2(moveInput.x, -moveInput.y);
    }
    private void UpdateRotation()
    {
        Vector3 PlayerDesiredDir = InputAxisToWorldDir(_moveInput);
        if (PlayerDesiredDir.magnitude == 0)
        {
            PlayerDesiredDir = transform.forward;
        }

        Quaternion DesiredRotation = Quaternion.LookRotation(PlayerDesiredDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, DesiredRotation, Time.deltaTime * _rotspeed);
    }
    internal void StopMovement() => _speed = 0;
    internal void BoostDeactive() => _isBoostActive = false;
    internal void BoostActive() => _isBoostActive = true;
    private Vector3 InputAxisToWorldDir(Vector2 input)
    {
        Vector3 CameraRight = Camera.main.transform.right;
        Vector3 FrameUp = Vector3.Cross(CameraRight, Vector3.up);

        return CameraRight * -input.y + -FrameUp * input.x;
    }
    #endregion
}

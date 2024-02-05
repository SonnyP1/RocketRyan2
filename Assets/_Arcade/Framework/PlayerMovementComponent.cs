using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] GameObject BoostEffect;
    [SerializeField] BoxCollider BoostCollider;
    [SerializeField] GameObject BoostFrontEffect;
    [SerializeField] AudioSource BoostSoundEffect;


    private float _speed = 5f;
    private float _rotspeed = 5f;
    private float _boostCurrent = 0f;
    private float boostMax = 100f;
    private Transform _boostTransform;

    private CharacterController _characterController;
    private GameplayUIManager _gameplayUIManager;

    private Vector2 _moveInput;
    private bool _isBoostActive = false;

    //Getters & Setters
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

    void Start()
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

    void Update()
    {
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

    public void ActivateBoostFrontEffect()
    {
        BoostFrontEffect.SetActive(true);
    }

    public void AddBoost(float value)
    {
        _boostCurrent = Mathf.Clamp(_boostCurrent + value,0,boostMax);
        UpdateBoostUI();
    }


    internal void UpdatedMoveInput(Vector2 moveInput)
    {
        _moveInput = new Vector2(moveInput.x, -moveInput.y);
    }
    
    void UpdateRotation()
    {
        Vector3 PlayerDesiredDir = InputAxisToWorldDir(_moveInput);
        if (PlayerDesiredDir.magnitude == 0)
        {
            PlayerDesiredDir = transform.forward;
        }

        Quaternion DesiredRotation = Quaternion.LookRotation(PlayerDesiredDir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, DesiredRotation, Time.deltaTime * _rotspeed);
    }

    internal void BoostDeactive()
    {
        _isBoostActive = false;
    }

    internal void BoostActive()
    {
        _isBoostActive = true;
    }

    void UpdateBoostUI()
    {
        _gameplayUIManager.UpdateBoostSlider(_boostCurrent / boostMax);
    }
    private Vector3 InputAxisToWorldDir(Vector2 input)
    {
        Vector3 CameraRight = Camera.main.transform.right;
        Vector3 FrameUp = Vector3.Cross(CameraRight, Vector3.up);

        return CameraRight * -input.y + -FrameUp * input.x;
    }

    internal void StopMovement()
    {
        _speed = 0;
    }

}

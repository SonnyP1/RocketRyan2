using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float RotationSpeed = 5f;
    [SerializeField] Transform BoostTransform;
    [SerializeField] GameObject BoostEffect;
    [SerializeField] BoxCollider BoostCollider;
    CharacterController _characterController;
    GameplayUIManager _gameplayUIManager;
    float _boostCurrent = 0f;
    float boostMax = 100f;
    Vector2 _moveInput;
    bool _isBoostActive = false;

    internal float GetCurrentBoost()
    {
        return _boostCurrent;
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        BoostCollider.enabled = false;
        if(scoreKeeper == null)
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
            _characterController.Move(-transform.right * (MoveSpeed*2) * Time.deltaTime);
            GameObject newBoostEffect = Instantiate(BoostEffect,BoostTransform);
            newBoostEffect.transform.parent = null;
            UpdateBoostUI();
            BoostCollider.enabled = true;
            return;
        }
        BoostCollider.enabled = false;
        _characterController.Move(-transform.right * MoveSpeed * Time.deltaTime);
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
        transform.rotation = Quaternion.Lerp(transform.rotation, DesiredRotation, Time.deltaTime * RotationSpeed);
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
        MoveSpeed = 0;
    }

}

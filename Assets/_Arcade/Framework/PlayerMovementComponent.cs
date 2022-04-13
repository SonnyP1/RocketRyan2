using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float RotationSpeed = 5f;
    CharacterController _characterController;
    GameplayUIManager _gameplayUIManager;
    public float boostCurrent = 100f;
    float boostMax = 100f;
    Vector2 _moveInput;
    bool _isBoostActive = false;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
    }
    void Update()
    {
        UpdateRotation();
        if(_isBoostActive == true && boostCurrent >= 0)
        {
            boostCurrent -= 0.1f;
            _characterController.Move(-transform.right * (MoveSpeed*2) * Time.deltaTime);
            _gameplayUIManager.UpdateBoostSlider(boostCurrent / boostMax);
            return;
        }
        _characterController.Move(-transform.right * MoveSpeed * Time.deltaTime);
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

    private Vector3 InputAxisToWorldDir(Vector2 input)
    {
        Vector3 CameraRight = Camera.main.transform.right;
        Vector3 FrameUp = Vector3.Cross(CameraRight, Vector3.up);

        return CameraRight * -input.y + -FrameUp * input.x;
    }
}

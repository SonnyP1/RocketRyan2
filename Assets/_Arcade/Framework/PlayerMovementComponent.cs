using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementComponent : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 5f;
    [SerializeField] float RotationSpeed = 5f;
    CharacterController _characterController;
    Vector2 _moveInput;
    bool _isBoostActive = false;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        UpdateRotation();
        if(_isBoostActive == true)
        {

            _characterController.Move(-transform.right * (MoveSpeed*2) * Time.deltaTime);
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

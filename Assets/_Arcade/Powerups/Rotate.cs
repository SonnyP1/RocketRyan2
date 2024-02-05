using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float RotateSpeedY;
    [SerializeField] float RotateSpeedX;
    [SerializeField] float RotateSpeedZ;
    void Update()
    {
        transform.Rotate(0f,RotateSpeedY*Time.deltaTime,0f,Space.Self);
        transform.Rotate(RotateSpeedX * Time.deltaTime, 0f, 0f, Space.Self);
        transform.Rotate(0f, 0f, RotateSpeedZ * Time.deltaTime, Space.Self);
    }
}

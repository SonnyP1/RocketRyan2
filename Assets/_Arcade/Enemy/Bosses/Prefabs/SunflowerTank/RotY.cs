using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotY : MonoBehaviour
{
    public float m_rotSpeed;
    private void Update()
    {
        transform.localEulerAngles += new Vector3(0,1.0f * Time.deltaTime * m_rotSpeed,0);
    }
}

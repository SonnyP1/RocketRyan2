using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RocketRyan", menuName = "RocketRyan/Cart", order = 1)]
public class Cart : ScriptableObject
{
    [Header("Movement Stats")]
    public float m_moveSpeed;
    public float m_turnSpeed;


    [Header("Weapon Stats")]
    public GameObject m_projectileType;
    public GameObject m_bombType;
    public int m_maxBombs;

    [Header("Looks")]
    public GameObject m_cartModel;
    public Vector3 m_projectileOffset;
    public Vector3 m_boostOffset;
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class SunflowerTank : Enemy
{
    public int m_hp;
    public float m_movementSpeed;
    public float m_radius;
    public float m_firerate;

    public GameObject m_projectile;
    public Transform[] m_spawnPoints;
    public float m_projectileSpeed;

    private float _maxStepDistance = 200f;

    private Vector3 _ptn;
    private Vector3 _dir = Vector3.zero;

    private int _maxHP;

    private void Start()
    {
        _maxHP = m_hp;

        base.Start();

        StartMovement();
        StartCoroutine(StartFire());
    }

    public override void BlowUp()
    {
        m_hp -= 1;
        ScoreKeeper.m_scoreKeeper.GetGameplayUIManager().UpdateBossUI(true,m_hp,_maxHP);

        if(m_hp <= 0)
        {
            ScoreKeeper.m_scoreKeeper.GetGameplayUIManager().UpdateBossUI(false,m_hp,_maxHP);
            base.BlowUp();
        }
    }

    private void Update()
    {
        transform.position += _dir * m_movementSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position, _ptn) < m_radius)
        {
            _dir = ReflectCast();
        }
    }

    private void StartMovement()
    {
        RaycastHit hit;
        _dir = transform.forward;
        if (Physics.Raycast(transform.position, _dir, out hit, _maxStepDistance))
        {
            Debug.DrawLine(transform.position, hit.point);
            _dir = hit.point - transform.position;
        }

        _dir = _dir.normalized;
        _ptn = hit.point;
    }
    private Vector3 ReflectCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, _dir, out hit, _maxStepDistance))
        {
            Debug.DrawLine(transform.position, hit.point);
            _dir = hit.point - transform.position;
        }

        Vector3 newDir = Vector3.Reflect(_dir, hit.normal).normalized;

        if (Physics.Raycast(transform.position,newDir,out hit))
        {
            _ptn = hit.point;
        }

        return newDir;
    }

    private void Fire()
    {
        foreach(Transform m_spawn in m_spawnPoints)
        {
            GameObject projectile = Instantiate(m_projectile, m_spawn, false);
            projectile.transform.parent = null;
            projectile.GetComponent<Trap>().SetSpeed(m_projectileSpeed);
        }
    }

    IEnumerator StartFire()
    {
        while(true)
        {
            Fire();
            yield return new WaitForSeconds(m_firerate);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SunflowerTank))]
class SunflowerTankEditor : Editor
{
    private void OnSceneGUI()
    {
        SunflowerTank m_tank = (SunflowerTank)target;
        Handles.color = Color.yellow;
        Handles.DrawWireArc(m_tank.transform.position, Vector3.up, Vector3.right, 360.0f, m_tank.m_radius);   
    }
}
#endif
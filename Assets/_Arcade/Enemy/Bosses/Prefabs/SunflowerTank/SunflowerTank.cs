using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class SunflowerTank : Enemy
{
    public int m_hp;
    public float m_radius;

    public GameObject m_projectile;
    public GameObject m_itemHelp;
    public Transform[] m_spawnPoints;
    public Transform[] m_itemSpawnPoints;


    //Phase Stats

    private int Phase = 0;

    public float PhaseTimer = 30.0f;
    private float currentTime;

    private float m_movementSpeed;
    private float m_firerate;
    private float m_projectileSpeed;

    public float m_Ph1MovementSpeed;
    public float m_Ph1Firerate;
    public float m_Ph1ProjectileSpeed;

    public float m_Ph2MovementSpeed;
    public float m_Ph2Firerate;
    public float m_Ph2ProjectileSpeed;

    public float m_Ph3MovementSpeed;
    public float m_Ph3Firerate;
    public float m_Ph3ProjectileSpeed;

    //

    private float _maxStepDistance = 200f;

    private Vector3 _ptn;
    private Vector3 _dir = Vector3.zero;

    private int _maxHP;

    private void Start()
    {
        _maxHP = m_hp;
        currentTime = PhaseTimer;

        //Phase 1 starts
        Phase = 1;
        m_movementSpeed = m_Ph1MovementSpeed;
        m_firerate = m_Ph1Firerate;
        m_projectileSpeed = m_Ph1ProjectileSpeed;
        Debug.Log("Phase 1 initiated");

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

        // Phase Timer
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            // change phase
            if(Phase == 1)
            {
                Phase = 2;
                m_movementSpeed = m_Ph2MovementSpeed;
                m_firerate = m_Ph2Firerate;
                m_projectileSpeed = m_Ph2ProjectileSpeed;
                Debug.Log("Phase 2 initiated");

            }
            else if(Phase == 2)
            {
                Phase = 3;
                m_movementSpeed = m_Ph3MovementSpeed;
                m_firerate = m_Ph3Firerate;
                m_projectileSpeed = m_Ph3ProjectileSpeed;
                Debug.Log("Phase 3 initiated");
            }
            else
            {
                Phase = 1;
                m_movementSpeed = m_Ph1MovementSpeed;
                m_firerate = m_Ph1Firerate;
                m_projectileSpeed = m_Ph1ProjectileSpeed;
                Debug.Log("Phase 1 initiated again");
            }
            currentTime = PhaseTimer;
            Debug.Log("Timer Reset");
        }

        // 
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

        foreach(Transform m_spawn in m_itemSpawnPoints)
        {
            GameObject itemHelp = Instantiate(m_itemHelp, m_spawn, false);
            itemHelp.transform.parent = null;
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
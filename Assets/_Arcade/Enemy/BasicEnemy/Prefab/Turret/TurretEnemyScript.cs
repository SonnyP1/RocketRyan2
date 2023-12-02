using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyScript : Enemy
{
    [Header("Projectiles Info")]
    [SerializeField] GameObject m_projectile;
    [SerializeField] Transform m_spawn;

    [Header("Stats")]
    [Range(0.1f,20f)] [SerializeField] float m_firerate;
    [Range(0.1f, 50f)][SerializeField] float m_projectileSpeed;

    public override void Start()
    {
        base.Start();
        SetNewTarget(FindObjectOfType<Player>().gameObject);
        StartCoroutine(FireRepeat());
    }

    private void Update()
    {
        if (GetTarget()) 
        { 
            transform.LookAt(GetTarget().transform);
            transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y,0f);
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(m_projectile,m_spawn,false);
        projectile.transform.parent = null;
        projectile.GetComponent<Trap>().SetSpeed(m_projectileSpeed);
    }

    IEnumerator FireRepeat()
    {
        while (true)
        {
            FireProjectile();
            yield return new WaitForSeconds(m_firerate);
        }

    }
}

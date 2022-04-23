using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BomberEnemy : Enemy
{
    [SerializeField] float ExplosionRadius;
    [SerializeField] SpriteRenderer spriteOne;
    [SerializeField] GameObject BomberEffect;
    [SerializeField] AudioSource BomberBlowUpSound;
    [SerializeField] AudioSource TickSound;
    Coroutine bomberTicker;
    NavMeshAgent _navMeshAgent;
    Player _player;
    int tickTimer = 0;
    override public void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
        bomberTicker = null;
    }

    private void Update()
    {
        if(tickTimer == 6)
        {
            ApplyExplosion();
            GameObject newEffect = Instantiate(BomberEffect, transform);
            newEffect.transform.parent = null;
            BomberBlowUpSound.Play();
            BomberBlowUpSound.transform.parent = null;
            base.BlowUp();
        }

        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
            return;
        }

        if(_navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }
    }

    private void ApplyExplosion()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider col in collidersInRange)
        {
            Enemy colAsEnemy = col.GetComponent<Enemy>();
            if (colAsEnemy)
            {
                colAsEnemy.BlowUp();
            }
            else
            {
                HealthComponent colAsHealth = col.GetComponent<HealthComponent>();
                if (colAsHealth)
                {
                    colAsHealth.AddToHealth(-1);
                }
            }
        }
    }

    public override void BlowUp()
    {
        if(bomberTicker == null)
        {
            bomberTicker = StartCoroutine(BomberTicking());
        }
    }
    IEnumerator BomberTicking()
    {
        yield return new WaitForSeconds(.5f);
        spriteOne.color = new Color(0, 0, 0,255);
        tickTimer++;
        TickSound.Play();
        yield return new WaitForSeconds(.5f);
        tickTimer++;
        TickSound.Play();
        spriteOne.color = Color.yellow;
        StartCoroutine(BomberTicking());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,ExplosionRadius);
    }
}

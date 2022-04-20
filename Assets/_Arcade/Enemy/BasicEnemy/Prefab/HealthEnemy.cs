using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealthEnemy : Enemy
{
    [SerializeField] float runAwayRadius;
    [SerializeField] GameObject HealthOrb;
    NavMeshAgent _navMeshAgent;
    override public void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        Collider[] objectInSphere =  Physics.OverlapSphere(transform.position, runAwayRadius);
        foreach(Collider obj in objectInSphere)
        {
            GameObject objAsGameObject = obj.gameObject;
            if(objAsGameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Vector3  playerDir =  transform.position -  objAsGameObject.transform.position;
                _navMeshAgent.SetDestination(playerDir * 5);
            }
        }
    }

    public override void BlowUp()
    {
        Instantiate(HealthOrb, transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, runAwayRadius);
    }
}

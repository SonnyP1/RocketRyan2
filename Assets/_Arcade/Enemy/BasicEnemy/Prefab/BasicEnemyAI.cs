using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : Enemy
{
    NavMeshAgent _navMeshAgent;
    Player _player;
    override public void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(_player == null)
        {
            _player = FindObjectOfType<Player>();
            return;
        }
        _navMeshAgent.SetDestination(_player.transform.position);
    }
}

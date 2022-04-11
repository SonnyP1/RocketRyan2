using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    Player _player;
    private void Start()
    {
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

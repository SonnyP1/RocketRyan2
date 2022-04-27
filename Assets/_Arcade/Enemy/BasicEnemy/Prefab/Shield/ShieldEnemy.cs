using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShieldEnemy : Enemy
{
    [SerializeField] Transform ShieldParent;
    [SerializeField] float ShieldRotSpeed;
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
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
            return;
        }
        if(GetTarget() == null)
        {
            SetNewTarget(_player.gameObject);
        }
        _navMeshAgent.SetDestination(GetTarget().transform.position);


        ShieldParent.Rotate(new Vector3(0, 1, 0), ShieldRotSpeed * Time.deltaTime);
    }
}

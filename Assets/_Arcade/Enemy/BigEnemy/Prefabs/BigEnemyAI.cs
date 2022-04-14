using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigEnemyAI : MonoBehaviour
{
    [SerializeField] float SpawnTime = 2f;
    [SerializeField] GameObject EnemyToSpawn;
    [SerializeField] float RandomRadius;
    [SerializeField] Transform[] PointsToSpawnEnemy;
    [SerializeField] SphereCollider SpawnArea;
    NavMeshAgent _navMeshAgent;
    Player _player;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
        SpawnArea.radius = RandomRadius;
        StartCoroutine(SpawnEnemy());
    }

    private void Update()
    {
        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
            return;
        }

        SetNewDestination();

    }

    private void SetNewDestination()
    {
        if(_navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial || _navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            _navMeshAgent.SetDestination(FindNewDestinationWithinSphere());
        }

        if (transform.position == _navMeshAgent.pathEndPosition)
        {
            _navMeshAgent.SetDestination(FindNewDestinationWithinSphere());
        }
    }

    private Vector3 FindNewDestinationWithinSphere()
    {
        float xPos = Random.Range(SpawnArea.bounds.min.x, SpawnArea.bounds.max.x);
        float zPos = Random.Range(SpawnArea.bounds.min.z, SpawnArea.bounds.max.z);

        Vector3 randomXZ = new Vector3(xPos, 0, zPos);

        return randomXZ;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, RandomRadius);
        Gizmos.DrawWireSphere(PointsToSpawnEnemy[0].position, 0.2f);
    }


    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(SpawnTime);

            Collider[] thingsColliding = Physics.OverlapSphere(PointsToSpawnEnemy[0].position,0.2f);
            if(thingsColliding.Length == 1)
            {
                GameObject newEnemy = Instantiate(EnemyToSpawn, PointsToSpawnEnemy[0]);
                newEnemy.transform.parent = null;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigEnemyAI : Enemy
{
    [SerializeField] float SpawnTime = 2f;
    [SerializeField] GameObject[] EnemyToSpawn;
    [SerializeField] float RandomRadius;
    [SerializeField] Transform[] PointsToSpawnEnemy;
    [SerializeField] SphereCollider SpawnArea;
    NavMeshAgent _navMeshAgent;
    Player _player;
    [SerializeField] float spawningRate = 0f;

    public void SetSpawningRate(float diffucultlyIndex)
    {
        spawningRate = diffucultlyIndex/10;
        Debug.Log("Level spawning rate is : " + spawningRate);
    }
    override public void Start()
    {
        base.Start();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = FindObjectOfType<Player>();
        SpawnArea.radius = RandomRadius;
        StartCoroutine(SpawnEnemy());
    }

    public override void InitEnemy()
    {
        base.InitEnemy();
        if(GetScoreKeeper().GetDifficultyIndex() >= 5)
        {
            Debug.Log("SPAWING RATE CUT IN HALF");
            spawningRate = spawningRate/2;
        }
        if(GetScoreKeeper().GetDifficultyIndex() >= 10)
        {
            Debug.Log("SPAWING RATE CUT IN FOUR");
            spawningRate = spawningRate / 4;
        }
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
                float randomNum = (Random.Range(0.0f, 10f)) / 10;
                GameObject newEnemy = null;
                if (randomNum <= spawningRate)
                {
                    int randomEnemyTypeIndex = Random.Range(1, EnemyToSpawn.Length);
                    newEnemy = Instantiate(EnemyToSpawn[randomEnemyTypeIndex], PointsToSpawnEnemy[0]);
                }
                else
                {
                    newEnemy = Instantiate(EnemyToSpawn[0], PointsToSpawnEnemy[0]);
                }
                newEnemy.transform.parent = null;
            }
        }
    }
}

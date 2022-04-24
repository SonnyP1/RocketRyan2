using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapEnemy : Enemy
{
    [SerializeField] GameObject TrapToSpawn;
    [SerializeField] float SpawnRate;
    [SerializeField] float TrapActiveRadius;
    [SerializeField] Transform AttackParent;
    [SerializeField] float AttackRotSpeed;
    [SerializeField] Transform[] AttackSpawnPoints;
    Coroutine spawnCoroutine;
    void Update()
    {
        AttackParent.Rotate(new Vector3(0, 1, 0), AttackRotSpeed * Time.deltaTime);

        if (spawnCoroutine == null)
        {
            Collider[] colsInRange = Physics.OverlapSphere(transform.position, TrapActiveRadius);
            foreach (Collider col in colsInRange)
            {
                Player player = col.GetComponent<Player>();
                if (player)
                {
                    spawnCoroutine = StartCoroutine(SpawnTrap());
                }
            }
        }
    }

    IEnumerator SpawnTrap()
    {
        yield return new WaitForSeconds(SpawnRate);
        foreach (Transform trans in AttackSpawnPoints)
        {
            GameObject newTrap = Instantiate(TrapToSpawn, trans);
            newTrap.transform.parent = null;
        }
        spawnCoroutine = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, TrapActiveRadius);
    }
}

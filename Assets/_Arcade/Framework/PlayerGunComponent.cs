using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunComponent : MonoBehaviour
{
    [SerializeField] GameObject ProjectileToSpawn;
    [SerializeField] Transform ProjectileSpawnPoint;

    internal void Fire()
    {
        GameObject newObject = Instantiate(ProjectileToSpawn, ProjectileSpawnPoint);
        newObject.transform.parent = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunComponent : MonoBehaviour
{
    [SerializeField] GameObject ProjectileToSpawn;
    [SerializeField] Transform ProjectileSpawnPoint;
    [SerializeField] AudioSource ShootingSound;

    internal void Fire()
    {
        GameObject newObject = Instantiate(ProjectileToSpawn, ProjectileSpawnPoint);
        newObject.transform.parent = null;
        if(ShootingSound.isPlaying)
        {
            ShootingSound.Stop();
        }
        ShootingSound.Play();
    }
}

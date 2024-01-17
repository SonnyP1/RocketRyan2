using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunComponent : MonoBehaviour
{
    [SerializeField] GameObject ProjectileToSpawn;
    [SerializeField] Transform ProjectileSpawnPoint;
    [SerializeField] AudioSource ShootingSound;

    [SerializeField] int BomberAmmo = 3;
    [SerializeField] int BomberMaxAmmo = 3;
    [SerializeField] GameObject BombToSpawn;
    [SerializeField] Transform BombSpawnPoint;

    private void Start()
    {
        BomberAmmo = ScoreKeeper.m_scoreKeeper.GetCurrentBombAmmo();
        UpdateBombUI();
    }
    internal void Fire()
    {
        GameObject newObject = Instantiate(ProjectileToSpawn,ProjectileSpawnPoint);
        newObject.transform.parent = null;
        if(ShootingSound.isPlaying)
        {
            ShootingSound.Stop();
        }
        ShootingSound.Play();
    }

    internal void DropBomb()
    {
        if(BomberAmmo != 0)
        {
            BomberAmmo = Mathf.Clamp(BomberAmmo - 1, 0, BomberMaxAmmo);
            GameObject newObject = Instantiate(BombToSpawn, BombSpawnPoint);
            newObject.transform.parent = null;
            UpdateBombUI();
        }
    }

    public void AddBomb()
    {
        BomberAmmo = Mathf.Clamp(BomberAmmo + 1, 0, BomberMaxAmmo);
        UpdateBombUI();
    }


    private void UpdateBombUI()
    {
        ScoreKeeper.m_scoreKeeper.UpdateBombAmmo(BomberAmmo);
        ScoreKeeper.m_scoreKeeper.GetGameplayUIManager().UpdateBombUI(BomberAmmo);
    }
}

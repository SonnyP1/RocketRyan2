using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float ExplosionRadius;
    [SerializeField] GameObject BombEffect;
    [SerializeField] Material MaterialToEdit;
    [SerializeField] GameObject BombObject;
    [SerializeField] AudioSource TickSound;
    [SerializeField] GameObject PointLight;
    int tickTimer = 0;
    private void Start()
    {
        Material newAssignMat = new Material(MaterialToEdit);
        BombObject.GetComponent<Renderer>().material = newAssignMat;
        tickTimer = 0;
        Enemy[] allEnemies = FindAllEnemies();
        foreach(Enemy enemy in allEnemies)
        {
            enemy.SetNewTarget(gameObject);
        }
        StartCoroutine(BomberTicking());
    }

    private Enemy[] FindAllEnemies()
    {
        return FindObjectsOfType<Enemy>();
    }
    private void Update()
    {
        if (tickTimer == 6)
        {
            ApplyExplosion();
            GameObject newEffect = Instantiate(BombEffect, transform);
            newEffect.transform.parent = null;
            Destroy(gameObject);
        }
    }
    private void ApplyExplosion()
    {
        Collider[] collidersInRange = Physics.OverlapSphere(transform.position, ExplosionRadius);
        foreach (Collider col in collidersInRange)
        {
            Enemy colAsEnemy = col.GetComponent<Enemy>();
            if (colAsEnemy)
            {
                colAsEnemy.BlowUp();
            }
        }
    }
    IEnumerator BomberTicking()
    {
        yield return new WaitForSeconds(.5f);
        BombObject.GetComponent<Renderer>().material.color = new Color(0, 0, 0, 255);
        PointLight.SetActive(false);
        tickTimer++;
        if (TickSound != null)
        {
            TickSound.Play();
        }
        yield return new WaitForSeconds(.5f);
        tickTimer++;
        PointLight.SetActive(true);
        if (TickSound != null)
        {
            TickSound.Play();
        }
        BombObject.GetComponent<Renderer>().material.color = Color.yellow;
        StartCoroutine(BomberTicking());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ExplosionRadius);
    }
}

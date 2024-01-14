using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] AudioSource BlowUpSound;
    [SerializeField] AudioSource EnemyBlowUpSound;
    [SerializeField] GameObject ProjectileEffect;
    public GameObject ProjectileKillEffect;
    void Start()
    {
    }
    private void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("IgnoreThis") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Enemy>().BlowUp();
        }

        if(BlowUpSound != null && EnemyBlowUpSound != null)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") && EnemyBlowUpSound.isActiveAndEnabled)
            {
                EnemyBlowUpSound.Play();
                EnemyBlowUpSound.transform.parent = null;
                GameObject AnotherEffect = Instantiate(ProjectileKillEffect, transform);
                AnotherEffect.transform.parent = null;
                Destroy(gameObject);
            }
            else if(BlowUpSound.isActiveAndEnabled)
            {
                BlowUpSound.Play();
                BlowUpSound.transform.parent = null;
            }
        }
        GameObject newEffect = Instantiate(ProjectileEffect, transform);
        newEffect.transform.parent = null;
        Destroy(gameObject);
    }
}

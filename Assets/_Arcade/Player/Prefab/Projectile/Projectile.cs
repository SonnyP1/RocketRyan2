using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] AudioSource BlowUpSound;
    [SerializeField] GameObject ProjectileEffect;
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

        if(BlowUpSound != null)
        {
            BlowUpSound.Play();
            BlowUpSound.transform.parent = null;
        }
        GameObject newEffect = Instantiate(ProjectileEffect, transform);
        newEffect.transform.parent = null;
        Destroy(gameObject);
    }
}

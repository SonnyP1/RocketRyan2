using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostColliderScript : MonoBehaviour
{

    [SerializeField] AudioSource RamSound;
    public GameObject CollideEffect;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("IgnoreThis") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            RamSound.Play();
            GameObject newEffect = Instantiate(CollideEffect, transform);
            newEffect.transform.parent = null;
            other.GetComponent<Enemy>().BlowUp();

            //Destroy(gameObject);
        }

    }
}

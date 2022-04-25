using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("IgnoreThis") || other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            other.GetComponent<Enemy>().BlowUp();
        }

    }
}

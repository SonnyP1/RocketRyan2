using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthup : MonoBehaviour
{
    HealthComponent _healthComp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(_healthComp == null)
            {
                _healthComp = other.GetComponent<HealthComponent>();
                _healthComp.AddToHealth(3f);
                Destroy(gameObject);
            }
        }
    }
}

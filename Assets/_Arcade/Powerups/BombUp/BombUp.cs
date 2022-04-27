using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.GetComponent<PlayerGunComponent>().AddBomb();
            Destroy(gameObject);
        }
    }
}

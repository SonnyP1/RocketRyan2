using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombUp : MonoBehaviour
{
    PlayerGunComponent _playerGunComp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (_playerGunComp == null)
            {
                _playerGunComp = other.GetComponent<PlayerGunComponent>();
                _playerGunComp.AddBomb();
                Destroy(gameObject);
            }
        }
    }
}

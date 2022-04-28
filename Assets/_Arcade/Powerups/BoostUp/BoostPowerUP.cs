using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPowerUP : MonoBehaviour
{
    PlayerMovementComponent _playerMovementComp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(_playerMovementComp == null)
            {
                _playerMovementComp = other.GetComponent<PlayerMovementComponent>();
                if(_playerMovementComp != null)
                {
                    _playerMovementComp.AddBoost(10f);
                    Destroy(gameObject);
                }
            }

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostOverEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        other.GetComponent<Rigidbody>().useGravity = false;
        other.GetComponent<CharacterController>().enabled = true;
        other.GetComponent<PlayerMovementComponent>().enabled = true;
    }
}

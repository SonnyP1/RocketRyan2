using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostOver : MonoBehaviour
{
    [SerializeField] Transform pointToMoveTarget;
    [SerializeField] float HeightOfArcLaunch;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("BoostOver Start");
            other.GetComponent<PlayerMovementComponent>().ActivateBoostFrontEffect();
            other.GetComponent<PlayerMovementComponent>().enabled = false;
            other.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<Rigidbody>().useGravity = true;

            Vector3 TargetPos = pointToMoveTarget.position;
            Vector3 PLPos = other.transform.position;
            float displacementY = TargetPos.y - PLPos.y;
            Vector3 displacementXZ = new Vector3(TargetPos.x - PLPos.x, 0, TargetPos.z - PLPos.z);

            Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * -9.81f * HeightOfArcLaunch);
            Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * HeightOfArcLaunch / -9.81f) + Mathf.Sqrt(2 * (displacementY - HeightOfArcLaunch) / -9.81f));

            other.GetComponent<Rigidbody>().velocity =  velocityXZ + velocityY;
        }
    }
}

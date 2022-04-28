using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Enemy
{
    [SerializeField] float MoveSpeed = 5f;
    public override void Start()
    {

    }

    public override void BlowUp()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            return;
        }
        BlowUp();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Enemy
{
    [SerializeField] float m_speed = 5f;
    public override void Start()
    {

    }

    public void SetSpeed(float val)
    {
        m_speed = val;
    }

    public override void BlowUp()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.position += transform.forward * m_speed * Time.deltaTime;
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

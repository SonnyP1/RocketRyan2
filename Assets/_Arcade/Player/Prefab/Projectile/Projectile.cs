using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    void Start()
    {
    }
    private void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == LayerMask.NameToLayer("IgnoreThis"))
        {
            return;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Destroy Enemy");
            other.GetComponent<Enemy>().BlowUp();
        }
        Destroy(gameObject);
    }
}

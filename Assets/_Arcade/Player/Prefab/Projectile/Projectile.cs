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
}

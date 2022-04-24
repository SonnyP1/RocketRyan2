using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Enemy
{
    public override void Start()
    {

    }
    public override void BlowUp()
    {
        Debug.Log("Blow up Shield");
        Destroy(gameObject);
    }
}

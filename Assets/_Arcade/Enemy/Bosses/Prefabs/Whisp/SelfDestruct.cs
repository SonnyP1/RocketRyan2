using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update

    public float countDownTime = 60.0f;
    private float currentTime;

    void Start()
    {
        currentTime = countDownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Timer = 0");
        }

        if ( currentTime <=0)
        {
            Destroy(gameObject);
        }
    }
}

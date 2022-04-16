using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RainbowEffect : MonoBehaviour
{
    [SerializeField] Color[] myColors;
    [SerializeField] [Range(0f,1f)] float lerpTime;
    TextMeshProUGUI text;
    int colorIndex;
    float t = 0f;
    int len;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        len = myColors.Length;
    }

    // Update is called once per frame
    void Update()
    {
        text.color = Color.Lerp(text.color, myColors[colorIndex], lerpTime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t >.5f)
        {
            t = 0f;
            colorIndex++;
            colorIndex = (colorIndex >= len) ? 0 : colorIndex;
        }
    }
}

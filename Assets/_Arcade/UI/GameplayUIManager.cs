using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] Slider BoostSlider;
    [SerializeField] Slider HealthSlider;

    public void UpdateBoostSlider(float newPercent)
    {
        BoostSlider.value = newPercent;
    }

    internal void UpdateHealthSlider(float newPercent)
    {
        HealthSlider.value = newPercent;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] Slider BoostSlider;
    [SerializeField] Slider HealthSlider;
    [SerializeField] Text EnemyCountTxt;
    [SerializeField] Text ScoreCountTxt;

    public void UpdateBoostSlider(float newPercent)
    {
        BoostSlider.value = newPercent;
    }

    internal void UpdateHealthSlider(float newPercent)
    {
        HealthSlider.value = newPercent;
    }

    public void UpdateEnemyCountTxt(float newCount)
    {
        EnemyCountTxt.text = newCount.ToString();
    }

    public void UpdateScoreCountTxt(float newCount)
    {
        ScoreCountTxt.text = newCount.ToString();
    }
}

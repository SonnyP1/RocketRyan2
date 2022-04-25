using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] Image hurtImage;
    [SerializeField] Slider BoostSlider;
    [SerializeField] Slider HealthSlider;
    [SerializeField] Text EnemyCountTxt;
    [SerializeField] Text ScoreCountTxt;
    Color alpha;
    Coroutine hurtUIOverlayCore;

    private void Start()
    {
        alpha.a = 50f/255f;
    }

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

    internal void HurtUIActive()
    {
        hurtUIOverlayCore = StartCoroutine(HurtUIOverlay());
    }

    IEnumerator HurtUIOverlay()
    {
        hurtImage.color += alpha;
        yield return new WaitForSeconds(0.5f);
        hurtImage.color -= alpha;
        hurtUIOverlayCore = null;
    }
}

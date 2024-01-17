using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    [SerializeField] Image hurtImage;
    [SerializeField] Slider BoostSlider;
    [SerializeField] Slider HealthSlider;
    [SerializeField] Slider BossSlider;
    [SerializeField] TextMeshProUGUI EnemyCountTxt;
    [SerializeField] TextMeshProUGUI ScoreCountTxt;
    [SerializeField] Image[] BombImages;
    Color alpha;
    Coroutine hurtUIOverlayCore;

    private void Start()
    {
        alpha.a = 50f/255f;

        UpdateBossUI(false,0,0);
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
        if(hurtImage.color.a < .4)
        {
            hurtImage.color += alpha;
        }
        yield return new WaitForSeconds(0.5f);
        hurtImage.color -= alpha;
        hurtUIOverlayCore = null;
    }

    internal void UpdateBombUI(int bombAmmo)
    {
        foreach(Image image in BombImages)
        {
            image.enabled = false;
        }

        for(int i = 0; i<bombAmmo; i++)
        {
            BombImages[i].enabled = true;
        }
    }

    internal void UpdateBossUI(bool show, int hp,int maxHP)
    {
        BossSlider.transform.gameObject.SetActive(show);
        BossSlider.value = (float)hp / (float)maxHP;
    }
}

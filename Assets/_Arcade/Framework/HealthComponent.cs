using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] LayerMask DamagableLayerMask;
    [SerializeField] float MaxHP;
    GameplayUIManager _gameplayUIManager;
    float _currentHP =0;

    internal float GetCurrentHealth()
    {
        return _currentHP;
    }

    private void Start()
    {
        ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (scoreKeeper.GetCurrentGlobalHealth() > 0)
        {
            _currentHP = scoreKeeper.GetCurrentGlobalHealth();
        }
        else
        {
            _currentHP = MaxHP;
        }

        UpdateHealthUI();
    }
    public void AddToHealth(float newValue)
    {
        _currentHP = Mathf.Clamp(_currentHP + newValue,0,MaxHP);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        _gameplayUIManager.UpdateHealthSlider(_currentHP / MaxHP);
    }
    private void OnTriggerEnter(Collider other)
    {
        int otherLayerAsDigit = other.gameObject.layer;
        int LayerMaskData = DamagableLayerMask;

        if ((LayerMaskData & (1 << otherLayerAsDigit)) != 0)
        {
            AddToHealth(-1);
        }
    }

}

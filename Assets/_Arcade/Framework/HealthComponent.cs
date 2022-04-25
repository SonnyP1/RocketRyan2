using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] LayerMask DamagableLayerMask;
    [SerializeField] float MaxHP = 10f;
    [SerializeField] AudioSource HurtSound;
    GameplayUIManager _gameplayUIManager;
    ScoreKeeper _scoreKeeper;
    float _currentHP = 0;

    internal float GetCurrentHealth()
    {
        return _currentHP;
    }

    private void Start()
    {
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if(_scoreKeeper == null || _gameplayUIManager == null)
        {
            return;
        }
        else
        {
            _currentHP = _scoreKeeper.GetCurrentHealthGlobal();
        }

        if(_currentHP == 0)
        {
            _currentHP = MaxHP;
        }

        UpdateHealthUI();
    }
    public void AddToHealth(float newValue)
    {
        if(newValue < 0)
        {
            if(HurtSound != null)
            {
                HurtSound.Play();
            }
            _gameplayUIManager.HurtUIActive();
        }

        _currentHP = Mathf.Clamp(_currentHP + newValue,0,MaxHP);
        UpdateHealthUI();
        if(_currentHP == 0)
        {
            ScoreKeeper scoreKeeper = FindObjectOfType<ScoreKeeper>();
            scoreKeeper.PlayerDeath();
        }
    }

    public void UpdateHealthUI()
    {
        _scoreKeeper.UpdateHealthOnGlobal(_currentHP);
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

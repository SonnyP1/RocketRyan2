using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script handle a health component of either a player or enemy
/// NOTE : This script isn't controlling the health currenlty should change this also make audio go to audio manager
/// </summary>
public class HealthComponent : MonoBehaviour
{
    [SerializeField] LayerMask m_DamagableLayerMask;
    [SerializeField] float m_MaxHP = 10f;
    [SerializeField] AudioSource m_HurtSound;


    private GameplayUIManager _gameplayUIManager;
    private ScoreKeeper _scoreKeeper;
    private float _currentHP = 0;

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
            _currentHP = m_MaxHP;
        }

        UpdateHealthUI();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            AddToHealth(-1);
        }
    }
    public void AddToHealth(float newValue)
    {
        if(newValue < 0)
        {
            if(m_HurtSound != null)
            {
                m_HurtSound.Play();
            }
            _gameplayUIManager.HurtUIActive();
        }

        _currentHP = Mathf.Clamp(_currentHP + newValue,0f,m_MaxHP);
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
        _gameplayUIManager.UpdateHealthSlider(_currentHP / m_MaxHP);
    }

}

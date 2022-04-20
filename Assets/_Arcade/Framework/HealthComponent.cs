using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] LayerMask DamagableLayerMask;
    [SerializeField] float CurrentHP;
    [SerializeField] float MaxHP;
    GameplayUIManager _gameplayUIManager;
    internal void SetCurrentHealth(float currentHealthOfPlayer)
    {
        CurrentHP = currentHealthOfPlayer;
        if (_gameplayUIManager == null)
        {
            _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        }
        _gameplayUIManager.UpdateHealthSlider(CurrentHP / MaxHP);
    }

    internal float GetCurrentHealth()
    {
        return CurrentHP;
    }

    private void Start()
    {
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
    }
    public void AddToHealth(float newValue)
    {
        Debug.Log(CurrentHP +" Before dmg");
        CurrentHP = Mathf.Clamp(CurrentHP+newValue,0,MaxHP);
        _gameplayUIManager.UpdateHealthSlider(CurrentHP/MaxHP);
        Debug.Log(CurrentHP +" After dmg");
    }

    private void Update()
    {
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

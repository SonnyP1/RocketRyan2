using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] LayerMask DamagableLayerMask;
    [SerializeField] float CurrentHP;
    [SerializeField] float MaxHP;
    GameplayUIManager _gameplayUIManager;

    private void Start()
    {
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
    }
    private void AddToHealth(float newValue)
    {
        CurrentHP = Mathf.Clamp(CurrentHP+newValue,0,MaxHP);
        _gameplayUIManager.UpdateHealthSlider(CurrentHP/MaxHP);
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

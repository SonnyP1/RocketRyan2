using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer InGameSprite;
    [SerializeField] SpriteRenderer VictorySprite;
    private void Start()
    {
        InGameSprite.color = new Color(255, 255, 255, 1);
        VictorySprite.color = new Color(255, 255, 255, 0);
    }
    internal void PlayVictoryAnimation()
    {
        InGameSprite.color = new Color(255, 255, 255,0);
        VictorySprite.color = new Color(255, 255, 255,1);
    }
}

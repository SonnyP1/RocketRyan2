using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer InGameSprite;
    [SerializeField] SpriteRenderer VictorySprite;
    [SerializeField] AudioSource VictorySound;
    [SerializeField] AudioSource Music;

    [SerializeField] SpriteRenderer EasterEgg;
    private void Start()
    {
        InGameSprite.color = new Color(255, 255, 255, 1);
        VictorySprite.color = new Color(255, 255, 255, 0);
    }

    internal void EasterEggActive()
    {
        InGameSprite.color = new Color(255, 255, 255,0);
        EasterEgg.color = new Color(255, 255, 255,1);
    }
    internal void PlayVictoryAnimation()
    {
        Music.Stop();
        VictorySound.Play();
        if(EasterEgg.color == new Color(255, 255, 255, 1))
        {
            EasterEgg.color = new Color(255, 255, 255,0);
        }
        InGameSprite.color = new Color(255, 255, 255,0);
        VictorySprite.color = new Color(255, 255, 255,1);
    }
}

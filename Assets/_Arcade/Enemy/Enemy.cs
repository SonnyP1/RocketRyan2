using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ScoreKeeper _scoreKeeper;
    virtual public void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        InitEnemy();
    }
    virtual public void InitEnemy()
    {
        if(_scoreKeeper == null)
        {
            return;
        }
        _scoreKeeper.AddToEnemyList(gameObject);
    }
    virtual public void BlowUp()
    {
        if (_scoreKeeper == null)
        {
            return;
        }
        _scoreKeeper.SubToEnemyList(gameObject);
        Destroy(gameObject);
    }
}

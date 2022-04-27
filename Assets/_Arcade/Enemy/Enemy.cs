using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int ScoreToAdd = 10;
    ScoreKeeper _scoreKeeper;
    GameObject target;
    public GameObject GetTarget()
    {
        return target;
    }
    public void SetNewTarget(GameObject newTarget)
    {
        target = newTarget;
    }
    public int GetScoreToAdd()
    {
        return ScoreToAdd;
    }
    public ScoreKeeper GetScoreKeeper()
    {
        return _scoreKeeper;
    }
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

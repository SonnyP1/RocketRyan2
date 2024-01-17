using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int ScoreToAdd = 10;
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

    virtual public void Start()
    {
        InitEnemy();
    }
    virtual public void InitEnemy()
    {
        ScoreKeeper.m_scoreKeeper.AddToEnemyList(gameObject);
    }
    virtual public void BlowUp()
    {
        ScoreKeeper.m_scoreKeeper.SubToEnemyList(gameObject);
        Destroy(gameObject);
    }
}

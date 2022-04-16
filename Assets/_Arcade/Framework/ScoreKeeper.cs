using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    List<GameObject> AllEnemies = new List<GameObject>();
    GameplayUIManager _gameplayUIManager;
    int score = 0;

    private void Start()
    {
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        _gameplayUIManager.UpdateScoreCountTxt(score);
    }
    public void AddToEnemyList(GameObject newEnemy)
    {
        AllEnemies.Add(newEnemy);
        if(_gameplayUIManager == null)
        {
            _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        }
        _gameplayUIManager.UpdateEnemyCountTxt(AllEnemies.Count);
    }

    public void SubToEnemyList(GameObject enemyToSub)
    {
        if(AllEnemies.Contains(enemyToSub))
        {
            AllEnemies.Remove(enemyToSub);
            score += 10;
        }
        _gameplayUIManager.UpdateEnemyCountTxt(AllEnemies.Count);
        _gameplayUIManager.UpdateScoreCountTxt(score);
    }
}

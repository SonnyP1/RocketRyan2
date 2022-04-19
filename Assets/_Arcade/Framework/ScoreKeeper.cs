using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    List<GameObject> AllEnemies = new List<GameObject>();
    GameplayUIManager _gameplayUIManager;
    int levelIndex = 1;
    int score = 0;
    float difficultyIndex = 0;

    private void Start()
    {
        Debug.Log("Called scorekeeper start");
        DontDestroyOnLoad(gameObject);
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        if(_gameplayUIManager == null)
        {
            return;
        }
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

    private void Update()
    {
        if(AllEnemies.Count == 0 && SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByName("MainMenu_Scene").buildIndex)
        {
            levelIndex++;
            levelIndex = (levelIndex >= SceneManager.sceneCountInBuildSettings) ? 1 : levelIndex;
            difficultyIndex++;
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
            SceneManager.sceneLoaded += OnNewLevelLoad;
        }
    }

    private void OnNewLevelLoad(Scene arg0, LoadSceneMode arg1)
    {
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        _gameplayUIManager.UpdateScoreCountTxt(score);

        BigEnemyAI[] allBigEnemies = FindObjectsOfType<BigEnemyAI>();
        foreach(BigEnemyAI bigEnemy in allBigEnemies)
        {
            bigEnemy.SetSpawningRate(difficultyIndex);
        }
    }
}

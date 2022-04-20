using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    List<GameObject> AllEnemies = new List<GameObject>();
    GameplayUIManager _gameplayUIManager;
    Player _player;
    int levelIndex = 1;
    int score = 0;
    float difficultyIndex = 0;
    bool isPlayerVictoryScreen = false;
    float currentHealthOfPlayer = 0;
    float currentBoost = 0;

    private void Start()
    {
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
        if(!isPlayerVictoryScreen && AllEnemies.Count == 0 && SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByName("MainMenu_Scene").buildIndex)
        {
            levelIndex++;
            levelIndex = (levelIndex >= SceneManager.sceneCountInBuildSettings) ? 1 : levelIndex;
            difficultyIndex++;

            StartCoroutine(PlayerVictoryScream());
        }
    }

    private void OnNewLevelLoad(Scene arg0, LoadSceneMode arg1)
    {
        _player = FindObjectOfType<Player>();
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        _gameplayUIManager.UpdateScoreCountTxt(score);

        _player.GetComponent<HealthComponent>().SetCurrentHealth(currentHealthOfPlayer);
        _player.GetComponent<PlayerMovementComponent>().SetCurrentBoost(currentBoost);

        BigEnemyAI[] allBigEnemies = FindObjectsOfType<BigEnemyAI>();
        foreach(BigEnemyAI bigEnemy in allBigEnemies)
        {
            bigEnemy.SetSpawningRate(difficultyIndex);
        }
        isPlayerVictoryScreen = false;
    }

    IEnumerator PlayerVictoryScream()
    {
        isPlayerVictoryScreen = true;
        if(_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        currentHealthOfPlayer = _player.GetComponent<HealthComponent>().GetCurrentHealth();
        currentBoost = _player.GetComponent<PlayerMovementComponent>().GetCurrentBoost();

        _player.DisablePlayerControls();

        _player.GetComponent<PlayerMovementComponent>().StopMovement();
        _player.GetComponent<PlayerAnimatorHandler>().PlayVictoryAnimation();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnNewLevelLoad;
    }
}

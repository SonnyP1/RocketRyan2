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
    bool isPlayerVictoryScreen = false;
    float currentHealthOfPlayer = 0;
    float currentBoost = 0;

    public float GetDifficultyIndex()
    {
        return difficultyIndex;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnNewLevelLoad;
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
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        _gameplayUIManager.UpdateScoreCountTxt(score);

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
        Player player = FindObjectOfType<Player>();
        currentHealthOfPlayer = player.GetComponent<HealthComponent>().GetCurrentHealth();
        currentBoost = player.GetComponent<PlayerMovementComponent>().GetCurrentBoost();

        player.DisablePlayerControls();

        player.GetComponent<PlayerMovementComponent>().StopMovement();
        player.GetComponent<PlayerAnimatorHandler>().PlayVictoryAnimation();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        SceneManager.sceneLoaded -= OnNewLevelLoad;
        Destroy(gameObject);
    }

    public float GetCurrentGlobalHealth()
    {
        return currentHealthOfPlayer;
    }
    public float GetCurrentBoostGlobal()
    {
        return currentBoost;
    }
}

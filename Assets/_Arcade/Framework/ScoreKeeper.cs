using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper m_scoreKeeper { get; private set; }
    private void Awake()
    {
        if (m_scoreKeeper != null && m_scoreKeeper != this)
        {
            Destroy(this);
        }
        else
        {
            m_scoreKeeper = this;
        }
    }


    List<GameObject> AllEnemies = new List<GameObject>();
    GameplayUIManager _gameplayUIManager;
    int levelIndex = 2;
    int score = 0;
    float difficultyIndex = 0;
    bool isPlayerVictoryScreen = false;
    float currentHealthOfPlayer = 0;
    float currentBoost = 0;
    int currentBombAmmo = 0;
    string _displayName;

    public GameplayUIManager GetGameplayUIManager()
    {
        return _gameplayUIManager;
    }
    public void SetDisplayName(string newDisplayName)
    {
        _displayName = newDisplayName;
    }

    public float GetDifficultyIndex()
    {
        return difficultyIndex;
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            levelIndex = SceneManager.GetActiveScene().buildIndex;
        }

        StartCoroutine(CheckForVictory());
        SceneManager.sceneLoaded += OnNewLevelLoad;
        DontDestroyOnLoad(gameObject);
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        if(_gameplayUIManager == null)
        {
            return;
        }
        _gameplayUIManager.UpdateScoreCountTxt(score);
    }

    internal float GetCurrentHealthGlobal()
    {
        return currentHealthOfPlayer;
    }
    public void UpdateHealthOnGlobal(float currentHP)
    {
        currentHealthOfPlayer = currentHP;
    }

    public void UpdateBombAmmo(int currentAmmo)
    {
        currentBombAmmo = currentAmmo;
    }
    internal int GetCurrentBombAmmo()
    {
        return currentBombAmmo;
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
            score += enemyToSub.GetComponent<Enemy>().GetScoreToAdd();
        }
        _gameplayUIManager.UpdateEnemyCountTxt(AllEnemies.Count);
        _gameplayUIManager.UpdateScoreCountTxt(score);
    }

    IEnumerator CheckForVictory()
    {
        yield return new WaitForSeconds(0.5f);
        while(true)
        {
            if (!isPlayerVictoryScreen && AllEnemies.Count == 0 && SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByName("MainMenu_Scene").buildIndex)
            {
                levelIndex++;
                levelIndex = (levelIndex >= SceneManager.sceneCountInBuildSettings) ? 2 : levelIndex;
                difficultyIndex++;

                StartCoroutine(PlayerVictoryScream());
            }
            yield return new WaitForEndOfFrame();
        }
    }
    private void OnNewLevelLoad(Scene arg0, LoadSceneMode arg1)
    {
        StartCoroutine(CheckForVictory());
        if (arg0.buildIndex == 1)
        {
            FindObjectOfType<PlayfabManager>().Login(score,_displayName);
            FindObjectOfType<Leaderboard>().UpdateLastScore(score);
            SceneManager.sceneLoaded -= OnNewLevelLoad;
            Destroy(gameObject);
            return;
        }

        if(score == 5000)
        {
            Player player = FindObjectOfType<Player>();
            player.GetComponent<PlayerAnimatorHandler>().EasterEggActive();
        }
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
        player.DisablePlayerControls();

        currentBoost = player.GetComponent<PlayerMovementComponent>().GetCurrentBoost();
        player.GetComponent<PlayerMovementComponent>().StopMovement();
        player.GetComponent<PlayerAnimatorHandler>().PlayVictoryAnimation();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public float GetCurrentBoostGlobal()
    {
        return currentBoost;
    }
}

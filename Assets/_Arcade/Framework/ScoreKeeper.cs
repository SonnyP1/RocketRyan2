using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper m_scoreKeeper { get; private set; }

    private GameplayUIManager _gameplayUIManager;
    private List<GameObject> _allEnemies = new List<GameObject>();

    private int _levelIndex = 2;
    private int _score = 0;
    private int _currentBombAmmo = 0;

    private float _difficultyIndex = 0;
    private float _currentHealthOfPlayer = 0;
    private float _currentBoost = 0;

    private string _displayName;

    private bool _isPlayerVictoryScreen = false;


    #region Getters & Setters
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
        return _difficultyIndex;
    }
    public float GetCurrentBoostGlobal()
    {
        return _currentBoost;
    }
    internal float GetCurrentHealthGlobal()
    {
        return _currentHealthOfPlayer;
    }
    public void UpdateHealthOnGlobal(float currentHP)
    {
        _currentHealthOfPlayer = currentHP;
    }
    public void UpdateBombAmmo(int currentAmmo)
    {
        _currentBombAmmo = currentAmmo;
    }
    internal int GetCurrentBombAmmo()
    {
        return _currentBombAmmo;
    }
    #endregion

    #region Unity Functions
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
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0)
        {
            _levelIndex = SceneManager.GetActiveScene().buildIndex;
        }

        StartCoroutine(CheckForVictory());
        SceneManager.sceneLoaded += OnNewLevelLoad;
        DontDestroyOnLoad(gameObject);
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        if(_gameplayUIManager == null)
        {
            return;
        }
        _gameplayUIManager.UpdateScoreCountTxt(_score);
    }
    private void OnNewLevelLoad(Scene arg0, LoadSceneMode arg1)
    {
        StartCoroutine(CheckForVictory());
        if (arg0.buildIndex == 1)
        {
            FindObjectOfType<PlayfabManager>().Login(_score,_displayName);
            FindObjectOfType<Leaderboard>().UpdateLastScore(_score);
            SceneManager.sceneLoaded -= OnNewLevelLoad;
            Destroy(gameObject);
            return;
        }

        if(_score == 5000)
        {
            Player player = FindObjectOfType<Player>();
            player.GetComponent<PlayerAnimatorHandler>().EasterEggActive();
        }
        _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        _gameplayUIManager.UpdateScoreCountTxt(_score);

        BigEnemyAI[] allBigEnemies = FindObjectsOfType<BigEnemyAI>();
        foreach(BigEnemyAI bigEnemy in allBigEnemies)
        {
            bigEnemy.SetSpawningRate(_difficultyIndex);
        }
        _isPlayerVictoryScreen = false;
    }


    #endregion

    //Custom Functions
    public void AddToEnemyList(GameObject newEnemy)
    {
        _allEnemies.Add(newEnemy);
        if(_gameplayUIManager == null)
        {
            _gameplayUIManager = FindObjectOfType<GameplayUIManager>();
        }
        _gameplayUIManager.UpdateEnemyCountTxt(_allEnemies.Count);
    }

    public void SubToEnemyList(GameObject enemyToSub)
    {
        if(_allEnemies.Contains(enemyToSub))
        {
            _allEnemies.Remove(enemyToSub);
            _score += enemyToSub.GetComponent<Enemy>().GetScoreToAdd();
        }
        _gameplayUIManager.UpdateEnemyCountTxt(_allEnemies.Count);
        _gameplayUIManager.UpdateScoreCountTxt(_score);
    }

    public void PlayerDeath()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    #region IEnumerators
    IEnumerator CheckForVictory()
    {
        yield return new WaitForSeconds(0.5f);
        while(true)
        {
            if (!_isPlayerVictoryScreen && _allEnemies.Count == 0 && SceneManager.GetActiveScene().buildIndex != SceneManager.GetSceneByName("MainMenu_Scene").buildIndex)
            {
                _levelIndex++;
                _levelIndex = (_levelIndex >= SceneManager.sceneCountInBuildSettings) ? 2 : _levelIndex;
                _difficultyIndex++;

                StartCoroutine(PlayerVictoryScream());
            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator PlayerVictoryScream()
    {
        _isPlayerVictoryScreen = true;
        Player player = FindObjectOfType<Player>();
        player.DisablePlayerControls();

        _currentBoost = player.GetComponent<PlayerMovementComponent>().GetCurrentBoost();
        player.GetComponent<PlayerMovementComponent>().StopMovement();
        player.GetComponent<PlayerAnimatorHandler>().PlayVictoryAnimation();
        yield return new WaitForSeconds(2f);
        Debug.Log("Scorekeeper wants to change scenes");
        //SceneManager.LoadScene(_levelIndex, LoadSceneMode.Single);
    }
    #endregion
}

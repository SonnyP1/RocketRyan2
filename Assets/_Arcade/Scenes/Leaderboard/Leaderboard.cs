using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Leaderboard : MonoBehaviour
{

    [SerializeField] PlayfabManager PlayFabManager;
    [SerializeField] Button BackToMainMenuBtn;
    [SerializeField] Button RefreshBtn;
    [SerializeField] TextMeshProUGUI LastScoreText;

    private void Start()
    {
        BackToMainMenuBtn.onClick.AddListener(OnMainMenuBtnClick);
        RefreshBtn.onClick.AddListener(OnRefreshBtnClick);
    }

    public void UpdateLastScore(int score)
    {
        LastScoreText.text = score.ToString();
    }

    private void OnRefreshBtnClick()
    {
        PlayFabManager.GetLeaderboard();
    }

    private void OnMainMenuBtnClick()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}

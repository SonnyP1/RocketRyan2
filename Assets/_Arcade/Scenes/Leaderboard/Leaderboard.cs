using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{

    [SerializeField] PlayfabManager PlayFabManager;
    [SerializeField] Button BackToMainMenuBtn;
    [SerializeField] Button RefreshBtn;

    private void Start()
    {
        BackToMainMenuBtn.onClick.AddListener(OnMainMenuBtnClick);
        RefreshBtn.onClick.AddListener(OnRefreshBtnClick);
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

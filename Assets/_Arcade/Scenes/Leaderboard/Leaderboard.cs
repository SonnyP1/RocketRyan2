using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] Button BackToMainMenuBtn;
    [SerializeField] Button RestartBtn;

    private void Start()
    {
        BackToMainMenuBtn.onClick.AddListener(OnMainMenuBtnClick);
        RestartBtn.onClick.AddListener(OnRestartBtnClick);
    }

    private void OnRestartBtnClick()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

    private void OnMainMenuBtnClick()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
}

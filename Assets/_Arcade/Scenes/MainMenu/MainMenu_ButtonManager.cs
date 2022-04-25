using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_ButtonManager : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    [SerializeField] Button LeaderBoardBtn;
    [SerializeField] Button ExitBtn;
    private void Start()
    {
        StartBtn.onClick.AddListener(StartBtnClicked);
        LeaderBoardBtn.onClick.AddListener(LeaderBoardBtnClicked);
        ExitBtn.onClick.AddListener(ExitBtnClicked);
    }
    private void StartBtnClicked()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    private void LeaderBoardBtnClicked()
    {
        //need to add this later
    }

    private void ExitBtnClicked()
    {
        Application.Quit();
    }
}

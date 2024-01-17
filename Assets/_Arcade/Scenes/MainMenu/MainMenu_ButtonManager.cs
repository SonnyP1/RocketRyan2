using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu_ButtonManager : MonoBehaviour
{
    [SerializeField] Button StartBtn;
    [SerializeField] Button LeaderBoardBtn;
    [SerializeField] Button ExitBtn;
    [SerializeField] TMP_InputField TextInputField;
    private void Start()
    {
        StartBtn.onClick.AddListener(StartBtnClicked);
        LeaderBoardBtn.onClick.AddListener(LeaderBoardBtnClicked);
        ExitBtn.onClick.AddListener(ExitBtnClicked);
        TextInputField.onValueChanged.AddListener(TextChange);
        ScoreKeeper.m_scoreKeeper.SetDisplayName(TextInputField.text);
    }

    private void TextChange(string stringChange)
    {
        ScoreKeeper.m_scoreKeeper.SetDisplayName(stringChange);
    }

    private void StartBtnClicked()
    {
        if(TextInputField.text.Length >= 1)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
    private void LeaderBoardBtnClicked()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void ExitBtnClicked()
    {
        Application.Quit();
    }
}

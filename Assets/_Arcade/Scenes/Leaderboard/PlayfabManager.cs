using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    [SerializeField] GameObject rowPrefab;
    [SerializeField] Transform rowParent;
    string displayName = "RocketRyan";
    int score = 0;
    public void Login(int newScore, string newName)
    {
        score = newScore;
        displayName = newName;
        Debug.Log(displayName);
        var request = new LoginWithCustomIDRequest
        {
            CustomId = displayName,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    private void OnSuccess(LoginResult obj)
    {
        Debug.Log("SUCCESS LOGIN");
        NewName();
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "RocketRyanScoreLeaderboard",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    private void NewName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult obj)
    {
        Debug.Log("Updated display name!");
        SendLeaderboard(score);
    }

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesfull leaderboard sent");
        GetLeaderboard();
    }


    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "RocketRyanScoreLeaderboard",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult results)
    {
        if(rowParent == null)
        {
            return;
        }


        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }
        
        int i = 0;
        foreach (var item in results.Leaderboard)
        {
            i++;
            if (i > 10)
            {
                break;
            }

            GameObject newRow = Instantiate(rowPrefab, rowParent);
            Text[] texts = newRow.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName.ToString();
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

}

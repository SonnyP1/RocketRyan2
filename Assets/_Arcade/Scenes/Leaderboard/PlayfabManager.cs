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
    [SerializeField]
    int score = 0;
    public void Login(int newScore)
    {
        score = newScore;

        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }
    private void OnSuccess(LoginResult obj)
    {
        Debug.Log("SUCCESS LOGIN");
        SendLeaderboard(score);
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

    private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Succesfull leaderboard sent");
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
        /*
        foreach(var item in results.Leaderboard)
        {
            Debug.Log($"{item.Position} {item.PlayFabId} {item.StatValue}");
        }
        */

        foreach (Transform item in rowParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in results.Leaderboard)
        {
            GameObject newRow = Instantiate(rowPrefab, rowParent);
            Text[] texts = newRow.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.PlayFabId.ToString();
            texts[2].text = item.StatValue.ToString();

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

}

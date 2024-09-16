using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RR_MultiplayerManager : NetworkBehaviour
{
    public static RR_MultiplayerManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void StartHost()
    {
        //NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
        Debug.Log("Start host called");
        NetworkManager.Singleton.StartHost();
    }

    private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest connectionApprovalRequest, 
                                                           NetworkManager.ConnectionApprovalResponse connectionApprovalResponse)
    {
        /* need to be able to handle different states of the game. ie: waiting to start the game, game is paused, waiting for countdown because otherwise this will just let people
           join any game. 
        */
        Debug.Log("is called");
        connectionApprovalResponse.Approved = true;
        connectionApprovalResponse.CreatePlayerObject = true;
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient(); 
    }
}

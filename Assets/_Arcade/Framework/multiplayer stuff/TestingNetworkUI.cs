using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetworkUI : MonoBehaviour
{
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(() => RR_MultiplayerManager.Instance.StartHost());
        clientButton.onClick.AddListener(() => RR_MultiplayerManager.Instance.StartClient());
        
    }
}

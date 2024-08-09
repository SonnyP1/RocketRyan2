using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RR_GameManager : MonoBehaviour
{
    public static RR_GameManager Instance {  get; private set; }
    private enum State
    {
        WaitingToStart,
        GamePlaying,
        GamePaused,
        GameOver,
    }

    private State state;
    [SerializeField] private float waitingToStartTimer;
    [SerializeField] private float countDownToStartTimer;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isGamePaused()
    {
        return state == State.GamePaused;
    }
}

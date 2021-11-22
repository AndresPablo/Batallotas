using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public FactionData[] factions;
    public Player[] players;
    [Space]
    public GameObject UI_Prefab;
    public GameState gameState;

    #region EVENTOS
    public delegate void OnGameEvent();
    public delegate void OnBoolEvent(bool state);
    public delegate void OnGameState(GameState _state);
	public static event OnGameEvent OnGameStart;
    public static event OnGameEvent OnGameOver;
    public static event OnGameEvent OnBeginLevelSetup;
    public static event OnBoolEvent OnTooglePause;
    public static event OnGameState OnGameStateChange;
    #endregion EVENTOS


    void Start()
    {
        Instantiate(UI_Prefab);    
    }

    public static void UnitTakeDamage(UnitLogic attacker, UnitLogic defender, float damage)
    {
        HealthLogic hp = defender.GetComponent<HealthLogic>();
    }

    public void SetupLevel()
    {
        if(OnBeginLevelSetup != null)
            OnBeginLevelSetup();

        // TODO:
        //Invoke("StartGame", .2f);
    }


    void Update()
    {
        // Pausar
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TooglePause();
        }
    }

    public void LaunchBattle()
    {
        SetGameState(GameState.PLAYING);
        if(OnGameStart != null)
            OnGameStart();
    }

    public void EndBattle(){}


    public void TooglePause()
    {
        if(gameState == GameState.LOSE)
            return;
        
        if(gameState == GameState.PAUSED)
        {
            gameState = GameState.PLAYING;
            if(OnTooglePause != null)
                OnTooglePause(false);
        }else
        {
            gameState = GameState.PAUSED;
            Time.timeScale = 0f;
            if(OnTooglePause != null)
                OnTooglePause(true);
        }

        if(OnGameStateChange != null) 
            OnGameStateChange(gameState);
    }

    void SetGameState(GameState _newState)
    {
        gameState = _newState;

        if(OnGameStateChange != null)
            OnGameStateChange(gameState);

        if(gameState == GameState.PLAYING)
        {
        }

        if(gameState == GameState.WIN)
        {
        }

        if(gameState == GameState.GAMEOVER)
        {
            if(OnGameOver != null) OnGameOver();
        }
    }
}

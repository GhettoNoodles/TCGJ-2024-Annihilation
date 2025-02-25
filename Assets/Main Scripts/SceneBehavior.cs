using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;



public class SceneBehaviour : MonoBehaviour
{
    public static SceneBehaviour Instance { get; private set; }
    private SessionInfo _session;
    [Header("Settings")] [SerializeField] private string sessionDataPath;
    [SerializeField] public int playerStartHealth;
    public int _p1Health { get; private set; }
    public int _p2Health{ get; private set; }
    private int nonGameScenes = 2;
    private int _currentGame;
    public int recentWinner;
    public float currentGametimer;
    public int[] gameScenes;

    //General Functions:------------------------------------------------------------------------------------------------
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            playerStartHealth = 4;
            Instance = this;
            Debug.Log("starting");
            _session = new SessionInfo();
            sessionDataPath = Application.streamingAssetsPath + "/inGameSaves/Session.json";
            ReadSessionInfo();
            Debug.Log(_session.gameIndex + "; " + _session.scores[0] + "; " + _session.scores[1]);
        }
    }

    private void ReadSessionInfo()
    {
        string jsonData = File.ReadAllText(sessionDataPath);
        _session = JsonConvert.DeserializeObject<SessionInfo>(jsonData);
        _currentGame = _session.gameIndex;
        _p1Health = _session.scores[0];
        _p2Health = _session.scores[1];
        gameScenes = _session.games;
        recentWinner = _session.recentWinner;

    }

    private void GetGameScenes()
    {
        gameScenes = new int[SceneManager.sceneCountInBuildSettings - nonGameScenes];
        var length = gameScenes.Length;
        for (int i = 0; i < length; i++)
        {
            gameScenes[i] = i + nonGameScenes;
        }

        for (int i = 0; i < length; i++)
        {
            var randA = Random.Range(0, length);
            var randB = Random.Range(0, length);
            var a = gameScenes[randA];
            var b = gameScenes[randB];
            gameScenes[randA] = b;
            gameScenes[randB] = a;
        }
    }

    private void Start()
    {
      
    }
    private void SaveSessionInfo()
    {
        _session.games = gameScenes;
        _session.scores = new []{_p1Health,_p2Health};
        _session.gameIndex = _currentGame;
        _session.recentWinner = recentWinner;
        string jsonData = JsonConvert.SerializeObject(_session);
        File.WriteAllText(sessionDataPath,jsonData);
    }
    //==================================================================================================================
    // Beginning of Session---------------------------------------------------------------------------------------------
    public void InitializeSession()
    {
        _p1Health = playerStartHealth;
        _p2Health = playerStartHealth;
        recentWinner = 0;
        GetGameScenes();
        _currentGame = 0;
        SaveSessionInfo();
    }
    //==================================================================================================================
    //Minigame Stuff----------------------------------------------------------------------------------------------------
    public void EndGameSession(Input_Manager.PlayerNumber winner)
    {
        if (winner == Input_Manager.PlayerNumber.P1)
        {
            _p2Health--;
            recentWinner = 1;
        }
        else
        {
            _p1Health--;
            recentWinner = 2;
        }

        _currentGame++;
        if (_currentGame == gameScenes.Length)
        {
            GetGameScenes();
            _currentGame = 0;
        }
        SaveSessionInfo();

            LoadInbetween();
        
        
       
    }

    public void LoadInbetween()
    {
        SceneManager.LoadScene(nonGameScenes - 1);
    }
    //==================================================================================================================
   //InBetweeners-------------------------------------------------------------------------------------------------------
    public void NextGame()
    {
        SceneManager.LoadScene(gameScenes[_currentGame]);
    }
    //==================================================================================================================
}
public class SessionInfo
{
    [JsonProperty("SessionType")] public string sessionType;
    [JsonProperty("Scores")] public int[] scores;
    [JsonProperty("Games")] public int[] games;
    [JsonProperty("currentGame")] public int gameIndex;
    [JsonProperty("recentWinner")] public int recentWinner;
}
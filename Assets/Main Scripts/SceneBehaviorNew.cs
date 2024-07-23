using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class SessionInfo
{
    [JsonProperty("SessionType")] public string sessionType;
    [JsonProperty("Scores")] public int[] scores;
    [JsonProperty("Games")] public int[] games;
    [JsonProperty("currentGame")] public int gameIndex;
}

public class SceneBehaviorNew : MonoBehaviour
{
    public static SceneBehaviorNew Instance { get; private set; }
    private SessionInfo _session;
    [Header("Settings")] [SerializeField] private string sessionDataPath;
    [SerializeField] private int playerStartHealth;
    private int _p1Health;
    private int _p2Health;
    private int _currentGame;

    public int[] gameScenes;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
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
        
    }

    private void GetGameScenes()
    {
        gameScenes = new int[SceneManager.sceneCountInBuildSettings - 1];
        var length = gameScenes.Length;
        for (int i = 0; i < length; i++)
        {
            gameScenes[i] = i + 1;
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

    // Start is called before the first frame update
    void Start()
    {
        _p1Health = playerStartHealth;
        _p2Health = playerStartHealth;
        GetGameScenes();
        _currentGame = 0;
        SaveSessionInfo();
    }

    private void SaveSessionInfo()
    {
        _session.games = gameScenes;
        _session.scores = new []{_p1Health,_p2Health};
        _session.gameIndex = _currentGame;
    }

    public void EndGame(Input_Manager.PlayerNumber winner)
    {
        ReadSessionInfo();
        if (winner == Input_Manager.PlayerNumber.P1)
        {
            _p2Health--;
        }
        else
        {
            _p1Health--;
        }

        _currentGame++;
        if (_currentGame == gameScenes.Length)
        {
            GetGameScenes();
            _currentGame = 0;
        }
        SaveSessionInfo();
    }

    public void NextGame()
    {
        ReadSessionInfo();
        SceneManager.LoadScene(_currentGame);
    }
    
}
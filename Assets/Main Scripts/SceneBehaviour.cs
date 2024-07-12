using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    public static SceneBehaviour Instance { get; private set; }

    public List<int> gameScenes = new List<int>();
    public List<int> loadedScenes = new List<int>();
    private int p1Health;
    private int p2Health;
    [SerializeField] private int startHealth;
    [SerializeField] private int damagePerGame;
    [SerializeField] private int gameSceneStartIndex;
    [SerializeField] private int FirstGameTimer;
    [SerializeField] private int GameTimeDecrement;
    public int GameTime;
    public Input_Manager.PlayerNumber recentWinner;

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

        GameObject[] objs = GameObject.FindGameObjectsWithTag("SceneBehaviour");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        GameTime = FirstGameTimer;
        p1Health = startHealth;
        p2Health = startHealth;
        GetGameScenes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGame();
        }
    }

    private void GetGameScenes()
    {
        for (int i = 2; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            gameScenes.Add(i);
        }

        Debug.Log("refreshed list");
    }

    public void EndGameSession(Input_Manager.PlayerNumber winner)
    {
        recentWinner = winner;
        if (winner == Input_Manager.PlayerNumber.P1)
        {
            p2Health -= damagePerGame;
        }
        else
        {
            p1Health -= damagePerGame;
        }

        if (p1Health <= 0)
        {
            GameOver(Input_Manager.PlayerNumber.P2);
        }
        else if (p2Health <= 0)
        {
            GameOver(Input_Manager.PlayerNumber.P1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    private void GameOver(Input_Manager.PlayerNumber loser)
    {
    }

    public void ChangeGame()
    {
        GameTime -= GameTimeDecrement;
        int randNum = Random.Range(0, gameScenes.Count);
        Debug.Log("changing game");
        SceneManager.LoadScene(gameScenes[randNum]);
        loadedScenes.Add(gameScenes[randNum]);
        gameScenes.RemoveAt(randNum);
       
        if (gameScenes.Count == 0)
        {
            gameScenes.Clear();
            GetGameScenes();
            loadedScenes.Clear();
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    public static SceneBehaviour Instance { get; private set; }

    public List<Scene> gameScenes = new List<Scene>();
    public List<Scene> loadedScenes = new List<Scene>();
    private int p1Health;
    private int p2Health;
    [SerializeField] private int startHealth;
    [SerializeField] private int damagePerGame;
    [SerializeField] private int gameSceneStartIndex;
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
        for (int i = gameSceneStartIndex; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            gameScenes.Add(SceneManager.GetSceneByBuildIndex(i));
        }
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

        if (p1Health<=0)
        {
            GameOver(Input_Manager.PlayerNumber.P2);
        }
        else if (p2Health<=0)
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
        int randNum = Random.Range(2, SceneManager.sceneCountInBuildSettings);

        loadedScenes.Add(SceneManager.GetSceneByBuildIndex(randNum));
        gameScenes.Remove(SceneManager.GetSceneByBuildIndex(randNum));
        if (gameScenes.Count==0)
        {
            GetGameScenes();
            loadedScenes.Clear();
        }
        SceneManager.LoadScene(randNum);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

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
    private Color p1Color;
    private Color p2Color = new Color(131f, 202f, 255f, 255f);
    [SerializeField] private Image p1ready;
    [SerializeField] private Image p2ready;
    private bool ingame = false;
    [SerializeField] private TextMeshProUGUI timertxt;
    private float timer = 0;
    private bool ready;
    [SerializeField] private GameObject readyPanel;

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
        if (!ingame)
        {
            p1ready.color = Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1)
                ? new Color(1, 0.5f, 0.5f, 1)
                : Color.white;

            p2ready.color = Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2)
                ? new Color(0.5f, 0.5f, 1, 1)
                : Color.white;
            ready = (Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P1) &&
                     Input_Manager.Instance.Get_Hold(Input_Manager.PlayerNumber.P2));
            if (ready)
            {
                timer -= Time.deltaTime;
                timertxt.text = timer.ToString("F0");
            }
            else
            {
                timertxt.text = "";
                timer = 3.49f;
            }

            if (timer <= 0.51f)
            {
                ChangeGame();
            }
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextGame();
        }
    }

    public void NextGame()
    {
        ingame = false;
        readyPanel.SetActive(true);
    }

    private void GetGameScenes()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
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
            SceneManager.LoadScene(0);
            if (ingame)
            {
                readyPanel.SetActive(false);
            }
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

    public void GameLoaded()
    {
        ingame = true;
        readyPanel.SetActive(false);
    }
}
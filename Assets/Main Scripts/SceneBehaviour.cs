using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    public static SceneBehaviour Instance { get; private set; }

    public List<Scene> gameScenes = new List<Scene>();
    [SerializeField] private int gameSceneStartIndex;

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
        GetGameScenes();
    }

    private void Update()
    {
        if( Input.GetKeyDown(KeyCode.Space) )
        {
            Debug.Log("changed Game");
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

    public void ChangeGame()
    {
        int randNum = Random.Range(gameSceneStartIndex, gameScenes.Count + 1);
        SceneManager.LoadScene(randNum);
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}

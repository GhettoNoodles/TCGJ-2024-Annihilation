using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool paused = false;

    [SerializeField]
    private GameObject PausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) &&
            paused == false)
        {
            PauseGAme();

        }

        else if ((Input.GetKey(KeyCode.Escape) &&
            paused == false))
        {
            ContinueGame();
        }
    }

    public void ContinueGame()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGAme()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

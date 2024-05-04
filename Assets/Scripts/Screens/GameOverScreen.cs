using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public void setup(){
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }
    public void RestartButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Main(){
        gameObject.SetActive(false);
         SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton(){
        Application.Quit(); 
    }



    private void Start()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeLevel();
            }
            else
            {
                PauseLevel();
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }


    public void ResumeLevel()
    {
        Time.timeScale = 1f;
        isPaused = false;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

        }
    }



    public void PauseLevel()
    {
        Time.timeScale = 0f;
        isPaused = true;

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }

}

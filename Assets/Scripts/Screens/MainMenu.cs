using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SelectMap selectmap;
    public void SetUp(){
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play(){
        gameObject.SetActive(false);
        selectmap.SetUp();
    }
    public void Quit(){
        Application.Quit();
    }

    public void getRules()
    {
        SceneManager.LoadScene("CUTSCENE");
    }
}

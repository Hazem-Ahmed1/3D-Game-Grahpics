using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
         //for test in unity
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

        
        // Application.Quit();
    }
}

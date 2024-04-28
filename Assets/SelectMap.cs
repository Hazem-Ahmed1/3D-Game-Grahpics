using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public MainMenu mainMenu;
    public void SetUp(){
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void Map1(){
        gameObject.SetActive(false);
        SceneManager.LoadScene("Map1");
    }
    
    public void Map2(){
        gameObject.SetActive(false);
        SceneManager.LoadScene("Map2");
    }
    public void Back(){
        gameObject.SetActive(false);
        mainMenu.SetUp();
    }
}

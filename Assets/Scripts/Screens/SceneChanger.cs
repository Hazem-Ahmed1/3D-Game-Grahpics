using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public float changeTime;
    private string sceneName = "MainMenu";

    [SerializeField] GameObject skipButton;

    private void Start()
    {
        skipButton.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime < 0)
        {
            SceneManager.LoadScene(sceneName);
        }

        Invoke("ShowSkipButton", 5f);
    }


    public void ShowSkipButton()
    {
        skipButton?.SetActive(true);
    }

    public void Skip()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

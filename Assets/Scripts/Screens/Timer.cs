using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject  gameOverScreen;
    public GameObject playerUI;
    public GameObject playerUI2;
    public GameObject win;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remaingTime;
    void Update()
    {
        if(remaingTime > 0){
            remaingTime -= Time.deltaTime;
        }
        else if(remaingTime <= 0){
            remaingTime = 0;
            
            playerUI.SetActive(false);
            playerUI2.SetActive(false);
            if(PlayerInteraction.score > AiAgent.scoreNPC){
                Cursor.lockState = CursorLockMode.None;
                win.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                gameOverScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            
        }
        
        int minutes = Mathf.FloorToInt(remaingTime/60);
        int seconds = Mathf.FloorToInt(remaingTime%60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
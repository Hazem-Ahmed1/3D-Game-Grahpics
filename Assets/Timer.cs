using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameOverScreen  gameOverScreen;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField]float remaingTime;
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        if(remaingTime > 0){
            remaingTime -= Time.deltaTime;
        }
        else if(remaingTime <= 0){
            remaingTime = 0;
            gameOverScreen.setup();
        }
        
        int minutes = Mathf.FloorToInt(remaingTime/60);
        int seconds = Mathf.FloorToInt(remaingTime%60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
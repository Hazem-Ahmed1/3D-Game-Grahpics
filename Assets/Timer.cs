using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameOverScreen  gameOverScreen;
    //public PlayerInteraction playerScore;
    public WinScreen win;
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
            if(PlayerInteraction.score > AiAgent.scoreNPC){
                win.setup(PlayerInteraction.score);
            }
            else{
                gameOverScreen.setup();
            }
            
        }
        
        int minutes = Mathf.FloorToInt(remaingTime/60);
        int seconds = Mathf.FloorToInt(remaingTime%60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }
}
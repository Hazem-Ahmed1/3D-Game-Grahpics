using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerInteraction : MonoBehaviour {
    public TextMeshProUGUI points;
    public KeyInventory keyInventory;
    public static int score;
    public treasureSpowner treasureSpowner;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip audioClip = null,coins;
    
    public void Strat(){
        score = 0;
        points.text = "X" + score.ToString();
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<AiAgent>() != null
        && other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey)
        {
            keyInventory.hasDoorLockedKey = true;
            other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey = false;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        if (other.gameObject.name.Equals("FinalGoal(Clone)") || other.gameObject.name.Equals("Goal(Clone)"))
        {
            Destroy(other.gameObject);
            treasureSpowner.SetupTreasureAndFlag();
            score++;
            points.text = "X" + score.ToString();
            audioSource.clip = coins;
            audioSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerInteraction : MonoBehaviour {
    public TextMeshProUGUI points;
    public KeyInventory keyInventory;
    public static int score;
    
    [SerializeField] private AudioSource Slap;
    [SerializeField] private AudioSource collect;

    [SerializeField] private GameObject treasureSpawnerGameObject;
    [SerializeField] private GameObject slapEffect; 
    
    public void Strat(){
        score = 0;
        points.text = score.ToString();
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<AiAgent>() != null
        && other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey && ComboAttack.isAttacking)
        {
            keyInventory.hasDoorLockedKey = true;
            other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey = false;
            Slap.Play();
            GameObject var = Instantiate(slapEffect,new Vector3(other.transform.position.x, other.transform.position.y+1, other.transform.position.z),Quaternion.identity);
            Destroy(var,1f);

        }

        if ((other.gameObject.name.Equals("FinalGoal(Clone)") || other.gameObject.name.Equals("Goal(Clone)")) && SceneManager.GetActiveScene().name.Equals("Map2"))
        {
            Destroy(other.gameObject);
            treasureSpawnerGameObject.GetComponent<treasureSpowner>().SetupTreasureAndFlag();
            score++;
            points.text =  score.ToString();
            collect.Play();
        }
        if ((other.gameObject.name.Equals("FinalGoal(Clone)") || other.gameObject.name.Equals("Goal(Clone)")) && SceneManager.GetActiveScene().name.Equals("Map1") )
        {
            Destroy(other.gameObject);
            treasureSpawnerGameObject.GetComponent<LevelTwoSpawnerTreasure>().SetupTreasureAndFlag();
            score++;
            points.text = score.ToString();
            collect.Play();
        }
    }
}

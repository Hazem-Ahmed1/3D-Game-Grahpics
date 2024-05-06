using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool Door = false;
    [SerializeField] private bool Key = false;
    [HideInInspector] private KeyInventory PlayerInventory = null;
    [HideInInspector]GameObject Player;
    private float distancePlayer;
    private KeyDoorController doorObject;

    [HideInInspector] private AudioSource audioSource = null;
    [SerializeField] private AudioClip audioClip = null;
    //public GameObject flag;

    private void Start()
    {
        Player = GameObject.Find("Duzzy");
        if (Door)
        {
            doorObject = GetComponent<KeyDoorController>();
            PlayerInventory = Player.GetComponentInChildren<KeyInventory>();
            audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        }
    }
    private void Update(){
        Player = GameObject.Find("Duzzy");
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        PlayerInventory = Player.GetComponentInChildren<KeyInventory>();
    }
    public void ObjectInteraction()
    {
        if (Door)
        {
            doorObject.PlayAnimation();
        }
        else if (Key)
        {
            distancePlayer = Vector3.Distance(Player.gameObject.transform.position, this.gameObject.transform.position);
            if (distancePlayer <= 5)
            {
                PlayerInventory.hasDoorLockedKey = true;
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            //flag.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}

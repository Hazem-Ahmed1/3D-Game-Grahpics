using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour {
    
    public KeyInventory keyInventory;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip audioClip = null;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<AiAgent>() != null
        && other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey)
        {
            keyInventory.hasDoorLockedKey = true;
            other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey = false;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}

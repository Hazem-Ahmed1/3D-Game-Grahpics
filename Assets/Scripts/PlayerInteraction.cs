using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour {
    
    public KeyInventory keyInventory;
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<AiAgent>() != null
        && other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey)
        {
            keyInventory.hasDoorLockedKey = true;
            other.gameObject.GetComponent<AiAgent>().hasDoorLockedKey = false;
            
        }
    }

}

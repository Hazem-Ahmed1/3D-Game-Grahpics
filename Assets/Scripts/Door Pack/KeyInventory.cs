using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    public bool hasDoorLockedKey = false;
    public GameObject KeyFlag;
    public GameObject BodyLight;

    [System.Obsolete]
    void Update(){
        KeyFlag.active = (hasDoorLockedKey)? true : false;
        BodyLight.active = (hasDoorLockedKey)? true : false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventListener : MonoBehaviour
{
    [SerializeField] private ComboAttack comboAttack;
    // Start is called before the first frame update
    void Start()
    {
        // comboAttack = GameObject.Find("Character").GetComponent<ComboAttack>();
    }
    public void AnimEvent()
    {
        // Debug.Log("Animation End Check");
        comboAttack.CheckAttackPhase();
    }
}

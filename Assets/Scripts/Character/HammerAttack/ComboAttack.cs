using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    Animator animator;
    public int CountAttackClick;
    private bool isAttacking;

    public ActiveWeapon active;
    void Start()
    {
        animator = GetComponent<Animator>();
        CountAttackClick = 0;
        isAttacking = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking && !active.weapon ) // Assuming left mouse button triggers the attack
        {
            StartCoroutine(StartAttack());
        }
    }

    IEnumerator StartAttack()
    {
        isAttacking = true;
        animator.SetLayerWeight(1, 1);
        BtnAttack();
        yield return new WaitForSeconds(0.1f); // Adjust this delay as needed
        isAttacking = false;
    }

    public void BtnAttack()
    {
        CountAttackClick++;
        // Debug.Log("CountAttackClick " + CountAttackClick);

        // 1st Attack Phase
        if (CountAttackClick == 1)
        {
            animator.SetInteger("intAttackPhase", 1);
            // CountAttackClick = 1;
        }
        // for avoiding stop animation
        // if (CountAttackClick >= 16)
        // {
        //     ResetAttackPhase();
        // }
    }

    // from the Listener
    public void CheckAttackPhase()
    {
        // Debug.Log("Checking Attack Phase...");
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack Horizontal"))
        {
            // Debug.Log("Current State : Attack 1");
            if (CountAttackClick > 1)
            {
                // next attack phase : 2
                animator.SetInteger("intAttackPhase", 2);
            }
            else
            {
                // Reset attack phase
                ResetAttackPhase();
            }
        }
        else if (animator.GetCurrentAnimatorStateInfo(1).IsName("Attack Downward"))
        {
            // Debug.Log("Current State : Attack 2");
            if (CountAttackClick >= 2)
            {
                ResetAttackPhase();
            }
        }
    }

    private void ResetAttackPhase()
    {
        CountAttackClick = 0;
        animator.SetInteger("intAttackPhase", 0);
        animator.SetLayerWeight(1, 0);
    }
}


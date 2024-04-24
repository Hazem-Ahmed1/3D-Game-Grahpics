using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    MovementStateManager movementStateManager;
    [SerializeField] GameObject Bottle;
    GameObject BoostEffect;
    [SerializeField] private int ExtremeBoost = 8; // Change the initial value to 8
    [SerializeField] private float BoostDuration = 5f;

    private float originalWalkSpeed;
    private float originalWalkBackSpeed;
    private float originalRunSpeed;
    private float originalRunBackSpeed;

    void Awake()
    {
        movementStateManager = GameObject.Find("Duzzy").GetComponent<MovementStateManager>();
        BoostEffect = GameObject.Find("SpeedLines");
    }
    private void Start()
    {
        // Save the original speeds when the Boost object is initialized
        originalWalkSpeed = movementStateManager.walkSpeed;
        originalWalkBackSpeed = movementStateManager.walkBackSpeed;
        originalRunSpeed = movementStateManager.runSpeed;
        originalRunBackSpeed = movementStateManager.runBackSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BoostEffect.GetComponent<ParticleSystem>().Play();
            BoostEffect.GetComponent<AudioSource>().Play();
            StartCoroutine(speedBoost());
        }
    }

    IEnumerator speedBoost()
    {
        // Apply the extreme boost
        movementStateManager.walkSpeed *= ExtremeBoost;
        movementStateManager.walkBackSpeed *= ExtremeBoost;
        movementStateManager.runSpeed *= ExtremeBoost;
        movementStateManager.runBackSpeed *= ExtremeBoost;

        Bottle.gameObject.SetActive(false);

        yield return new WaitForSeconds(BoostDuration);

        // Revert to the original speeds
        movementStateManager.walkSpeed = originalWalkSpeed;
        movementStateManager.walkBackSpeed = originalWalkBackSpeed;
        movementStateManager.runSpeed = originalRunSpeed;
        movementStateManager.runBackSpeed = originalRunBackSpeed;

        // Destroy the Boost object
        Destroy(this.gameObject);
        BoostEffect.GetComponent<ParticleSystem>().Stop();
        BoostEffect.GetComponent<AudioSource>().Stop();
    }
}

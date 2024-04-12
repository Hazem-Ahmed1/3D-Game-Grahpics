using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorController : MonoBehaviour
{
    private Animator doorAnim;
    private bool doorOpen = false;
    
    [Header("Animation Names")]
    [SerializeField] private string openAnimationName = "OpenDoor";
    [SerializeField] private string closeAnimationName = "CloseDoor";
    [SerializeField] private int timeToShowUI = 1;
    [SerializeField] private GameObject showDoorLockedUI = null;
    [SerializeField] private KeyInventory _keyInventory = null;
    [SerializeField] private int waitTimer = 1;
    [SerializeField] private bool pauseInteraction = false;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip audioClip = null;

    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    private IEnumerator PauseDoorInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimer);
        pauseInteraction = false;
    }
    public void PlayAnimation()
    {
        if (_keyInventory.hasRedKey)
        {
            OpenDoor();
        }
        else
        {
            StartCoroutine(ShowDoorLocked());
        }
    }
    void OpenDoor()
    {
        if (!doorOpen && !pauseInteraction)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            doorAnim.Play(openAnimationName, 0, 0.0f);
            doorOpen = true;
            StartCoroutine(PauseDoorInteraction());
            Physics.IgnoreLayerCollision(6,3, true);
        }
        else if(doorOpen && !pauseInteraction)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            doorAnim.Play(closeAnimationName, 0, 0.0f);
            doorOpen = false;
            StartCoroutine(PauseDoorInteraction());
            Physics.IgnoreLayerCollision(6,3, false);
        }
    }
    IEnumerator ShowDoorLocked()
    {
        showDoorLockedUI.SetActive(true);
        yield return new WaitForSeconds(timeToShowUI);
        showDoorLockedUI.SetActive(false);
    }
}

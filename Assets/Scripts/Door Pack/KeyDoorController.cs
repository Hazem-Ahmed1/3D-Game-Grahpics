using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorController : MonoBehaviour
{
    private Animator doorAnim;
    private float distancePlayer;
    [SerializeField]GameObject Player;
    private bool doorOpen = false;
    [SerializeField] private string openAnimationName = "OpenDoor";
    [SerializeField] private string closeAnimationName = "CloseDoor";
    [SerializeField] private int  TimeLockedUI = 1;
    [SerializeField] private GameObject showDoorLockedUI = null;
    [SerializeField]private KeyInventory PlayerInventory;
    [SerializeField] private int waitTimerAnimation = 1;
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
        yield return new WaitForSeconds(waitTimerAnimation);
        pauseInteraction = false;
    }
    public void PlayAnimation()
    {
        distancePlayer = Vector3.Distance(Player.gameObject.transform.position, this.gameObject.transform.position);
        if (PlayerInventory.hasDoorLockedKey && distancePlayer <= 5 )
        {
            OpenDoor();
        }
        else if(!PlayerInventory.hasDoorLockedKey && distancePlayer <= 5 && !doorOpen)
        {
            StartCoroutine(ShowDoorLocked());
        }
    }
    public void OpenDoor()
    {
        if (!doorOpen && !pauseInteraction)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            doorAnim.Play(openAnimationName, 0, 0.0f);
            doorOpen = true;
            StartCoroutine(PauseDoorInteraction());
            Physics.IgnoreLayerCollision(7,3, true);
        }
        else if(doorOpen && !pauseInteraction)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            doorAnim.Play(closeAnimationName, 0, 0.0f);
            doorOpen = false;
            StartCoroutine(PauseDoorInteraction());
            Physics.IgnoreLayerCollision(7,3, false);
        }
    }
    IEnumerator ShowDoorLocked()
    {
        showDoorLockedUI.SetActive(true);
        yield return new WaitForSeconds(TimeLockedUI);
        showDoorLockedUI.SetActive(false);
    }
}

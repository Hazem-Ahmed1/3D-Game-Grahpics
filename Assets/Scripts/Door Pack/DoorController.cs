using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator_Door;
    private bool DoorOpen = false;
    // [SerializeField] private AudioSource audioSource = null;
    // [SerializeField] private AudioClip audioClip = null;
    [SerializeField] private int waitTimerAnimation = 1;
    [SerializeField] private bool pauseInteraction = false;
    // Start is called before the first frame update
    void Start()
    {
        animator_Door = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!DoorOpen && animator_Door != null && !pauseInteraction) 
        {
            // audioSource.clip = audioClip;
            // audioSource.Play();
            animator_Door.Play("OpenDoor", 0, 0.0f);
            DoorOpen = true;
            StartCoroutine(PauseDoorInteraction());
            Physics.IgnoreLayerCollision(6, 3);
        }
        else if (DoorOpen && animator_Door != null && !pauseInteraction)
        {
            // audioSource.clip = audioClip;
            // audioSource.Play();
            animator_Door.Play("CloseDoor", 0, 0.0f);
            DoorOpen = false;
            StartCoroutine(PauseDoorInteraction());
            Physics.IgnoreLayerCollision(6, 3,false);
        }
    }
    
    private IEnumerator PauseDoorInteraction()
    {
        pauseInteraction = true;
        yield return new WaitForSeconds(waitTimerAnimation);
        pauseInteraction = false;
    }

}

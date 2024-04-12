using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator animator_Door;
    private bool DoorOpen = false;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip audioClip = null;
    // Start is called before the first frame update
    void Start()
    {
        animator_Door = gameObject.GetComponent<Animator>();

        
    }

    public void PlayAnimation()
    {
        if (!DoorOpen && animator_Door != null) 
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            animator_Door.Play("OpenDoor", 0, 0.0f);
            DoorOpen = true;
            Physics.IgnoreLayerCollision(6, 3);
        }
        else if (DoorOpen && animator_Door != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            animator_Door.Play("CloseDoor", 0, 0.0f);
            DoorOpen = false;
            Physics.IgnoreLayerCollision(6, 3,false);
        }
    }
    

}

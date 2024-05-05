using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuzzyDance : MonoBehaviour
{
    public Animator animator;
    public GameObject Duzzy;
    public GameObject Player;
    [SerializeField] AudioSource DanceSound;
    bool isDancing = false;

    // Reference to Duzzy's movement controller
    MovementStateManager movementController;

    // Start is called before the first frame update
    void Start()
    {
        animator = Player.GetComponent<Animator>();
        movementController = Duzzy.GetComponent<MovementStateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDancing)
        {
            if (!DanceSound.isPlaying)
            {
                DanceSound.Play();
            }
        }
        else
        {
            DanceSound.Stop();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Bullet_Dancing")
        {
            StartCoroutine(StartDance());
        }
    }

    IEnumerator StartDance()
    {
        // Disable Duzzy's movement controller
        movementController.enabled = false;

        animator.SetLayerWeight(animator.GetLayerIndex("DozyDancing"), 1);
        isDancing = true;
        yield return new WaitForSeconds(5f);
        isDancing = false;
        animator.SetLayerWeight(animator.GetLayerIndex("DozyDancing"), 0);

        // Re-enable Duzzy's movement controller
        movementController.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuzzyDance : MonoBehaviour
{
    public Animator animator;
    public GameObject Duzzy;
    public GameObject Player;
    public bool isDelayed = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Bullet_Dancing")
        {
            Duzzy.GetComponent<MovementStateManager>().enabled = false;
            animator.SetLayerWeight(animator.GetLayerIndex("DozyDancing"), 1);
            isDelayed = true;
            Invoke("ResetDance", 5f);
            // Destroy(collision.gameObject);
        }
    }

    private void ResetDance()
    {
        Duzzy.GetComponent<MovementStateManager>().enabled = true;
        animator.SetLayerWeight(animator.GetLayerIndex("DozyDancing"), 0);
        isDelayed = false;
    }
}
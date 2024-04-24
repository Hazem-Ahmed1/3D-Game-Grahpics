using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool Door = false;
    [SerializeField] private bool Key = false;
    [SerializeField] private KeyInventory PlayerInventory = null;
    // [SerializeField] private KeyInventory EnemyInventory = null;
    [SerializeField]GameObject Player;
    // [SerializeField]GameObject Enemy;
    // private string interactTableTagEnemy = "EnemyInteractiveObj";
    // private string interactTableTagPlayer = "Player";
    private float distancePlayer;
    // private float distanceEnemy;
    private KeyDoorController doorObject;

    private void Start()
    {
        if (Door)
        {
            doorObject = GetComponent<KeyDoorController>();
        }
    }

    public void ObjectInteraction()
    {
        if (Door)
        {
            doorObject.PlayAnimation();
        }
        else if (Key)
        {
            distancePlayer = Vector3.Distance(Player.gameObject.transform.position, this.gameObject.transform.position);
            // distanceEnemy = Vector3.Distance(Enemy.gameObject.transform.position, this.gameObject.transform.position);
            // Debug.Log(distanceEnemy);
            Debug.Log(distancePlayer);
            if (distancePlayer <= 5)
            {
                PlayerInventory.hasDoorLockedKey = true;
            }
            // else if (distanceEnemy <= 5)
            // {
            //     EnemyInventory.hasDoorLockedKey=true;
            // }
            gameObject.SetActive(false);
        }
    }
}

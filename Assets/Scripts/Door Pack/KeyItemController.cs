using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool Door = false;
    [SerializeField] private bool Key = false;
    [SerializeField] private KeyInventory PlayerInventory = null;
    [SerializeField] private KeyInventory EnemyInventory = null;
    private string interactTableTagEnemy = "EnemyInteractiveObj";
    private string interactTableTagPlayer = "Player";
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
            if (Door)
            {
                doorObject.PlayAnimation();
            }
            else if (Key)
            {
                if (hit.collider.CompareTag(interactTableTagPlayer))
                {
                    PlayerInventory.hasDoorLockedKey = true;
                    gameObject.SetActive(false);
                }
                else if (hit.collider.CompareTag(interactTableTagEnemy))
                {
                    EnemyInventory.hasDoorLockedKey = true;
                    gameObject.SetActive(false);
                }
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealKey : MonoBehaviour
{
    [SerializeField] public int rayLenght = 5;
    [SerializeField] private LayerMask LayerMaskInteract;
    [SerializeField] private string excluseLayerMask = null;
    [SerializeField] private Image crosshair = null ;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Player;
    private KeyInventory EnemyInventory;
    private KeyInventory PlayerInventory;
    private bool isCrosshairActive;
    private bool doOnce;
    private string interactAbleTagPlayer = "Player";
    private string interactAbleTagEnemy = "EnemyInteractiveObj";
    private KeyItemController raycastedObject;
    [SerializeField] private KeyCode stealDoorKey = KeyCode.Q;
    private bool key;
    // Start is called before the first frame update
    void Awake()
    {
        EnemyInventory = Enemy.GetComponent<KeyInventory>();
        PlayerInventory = Player.GetComponent<KeyInventory>();
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excluseLayerMask) | LayerMaskInteract.value;
        if (Physics.Raycast(transform.position,fwd, out hit,rayLenght,mask))
        {
            if(hit.collider.CompareTag(interactAbleTagPlayer) || hit.collider.CompareTag(interactAbleTagEnemy))
            {
                if (!doOnce)
                {
                    // raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                    CrossHairChange(true);
                }
                isCrosshairActive = true;
                doOnce = true;
                if (Input.GetKeyDown(stealDoorKey))
                {
                    if (EnemyInventory.hasDoorLockedKey && !PlayerInventory.hasDoorLockedKey)
                    {
                        EnemyInventory.hasDoorLockedKey = false;
                        PlayerInventory.hasDoorLockedKey = true;
                    }
                }
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                CrossHairChange(false);
                doOnce = false;
            }
        }
    }
    void CrossHairChange(bool on)
    {
        if (on && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }
}

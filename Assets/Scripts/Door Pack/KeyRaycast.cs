using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyRaycast : MonoBehaviour
{
    [SerializeField] public int rayLenght = 5;
    [SerializeField] private LayerMask LayerMaskInteract;
    [SerializeField] private string excluseLayerMask = null;
    private KeyItemController raycastedObject;
    [SerializeField] private KeyCode openDoorKey = KeyCode.F;
    [SerializeField] private GameObject Player;
    [SerializeField] private Image crosshair = null ;
    private bool isCrosshairActive;
    private bool doOnce;
    private string interactTableTagDoor = "DoorGoal";
    private string interactTableTagKey = "Key";

    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    void Awake()
    {
        interactionUI.SetActive(false);
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(excluseLayerMask) | LayerMaskInteract.value;
        if (Physics.Raycast(transform.position,fwd, out hit,rayLenght,mask))
        {
            // Door
            if(hit.collider.CompareTag(interactTableTagDoor))
            {
                if (!doOnce)
                {
                    raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                    CrossHairChange(true,true);
                }
                isCrosshairActive = true;
                doOnce = true;
                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastedObject.ObjectInteraction();
                }
            }
            // key
            else if(hit.collider.CompareTag(interactTableTagKey))
            {
                if (!doOnce)
                {
                    raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                    CrossHairChange(true,false);
                }
                isCrosshairActive = true;
                doOnce = true;
                if (Input.GetKeyDown(openDoorKey))
                {
                    raycastedObject.ObjectInteraction();
                }
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                CrossHairChange(false,false);
                doOnce = false;
            }
        }
    }
    void CrossHairChange(bool on, bool isDoor)
    {
        if (on && !doOnce)
        {
            interactionText.text = isDoor? "press" : "to grab press";
            interactionUI.SetActive(on);
            crosshair.enabled = false;
        }
        else
        {
            interactionUI.SetActive(false);
            crosshair.enabled = true;
            isCrosshairActive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int RayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string ExcludeLayerName = null;
    private DoorController RayCastedObj;
    [SerializeField] private KeyCode OpenDoorKey = KeyCode.E;
    [SerializeField] private Image CrossHair = null;
    private bool IsCrossHairActive;
    private bool DoOnce;
    private const string interactableTag = "DoorInteractiveObj";
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;
    private void Start()
    {
        interactionUI.SetActive(false);
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        int mask = 1 << LayerMask.NameToLayer(ExcludeLayerName) | layerMaskInteract.value;
        if (Physics.Raycast(transform.position,fwd, out hit,RayLength,mask))
        {
            if(hit.collider.CompareTag(interactableTag))
            {
                if(!DoOnce)
                {
                    RayCastedObj = hit.collider.gameObject.GetComponent<DoorController>();
                    CrossHairChange(true);
                }
                IsCrossHairActive = true;
                DoOnce = true; 
                if(Input.GetKeyDown(OpenDoorKey))
                {
                    RayCastedObj.PlayAnimation();
                }
            }
        }
        else
        {
            if(IsCrossHairActive)
            {
                CrossHairChange(false);
                DoOnce = false;
            }
        }
    }
    void CrossHairChange(bool on)
    {
        if(on && !DoOnce)
        {
            interactionText.text = "press";
            interactionUI.SetActive(on);
            CrossHair.enabled = false;
        }
        else
        {
            interactionUI.SetActive(false);
            CrossHair.enabled = true;
            IsCrossHairActive = false;
        }
    }
}

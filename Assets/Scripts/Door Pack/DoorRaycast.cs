using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorRaycast : MonoBehaviour
{
    [SerializeField] private int RayLength = 5;
    [SerializeField] private LayerMask layerMaskInteract;
    [SerializeField] private string ExcludeLayerName = null;

    private DoorController RayCastedObj;
    [SerializeField] private KeyCode OpenDoorKey = KeyCode.E;

    // private KeyItemController raycastedObject;
    // [SerializeField] private KeyCode DoorKey = KeyCode.F;
        
    [SerializeField] private Image CrossHair = null;
    private bool IsCrossHairActive;
    private bool DoOnce;
    private const string interactableTag = "DoorInteractiveObj";


    private void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        int mask = 1 << LayerMask.NameToLayer(ExcludeLayerName) | layerMaskInteract.value;
        // Debug.Log(1 << LayerMask.NameToLayer(ExcludeLayerName));
        // Debug.Log(layerMaskInteract.value);
        // Debug.Log("mask is : " + mask);



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
            CrossHair.color = Color.red;
        }
        else
        {
            CrossHair.color = Color.white;
            IsCrossHairActive = false;

        }
    }
}

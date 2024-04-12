using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyRaycast : MonoBehaviour
{
    [SerializeField] private int rayLenght = 5;
        [SerializeField] private LayerMask LayerMaskInteract;
        [SerializeField] private string excluseLayerMask = null;
        private KeyItemController raycastedObject;
        [SerializeField] private KeyCode openDoorKey = KeyCode.F;
        [SerializeField] private Image crosshair = null ;
        private bool isCrosshairActive;
        private bool doOnce;
        private string interactableTag = "DoorInteractiveObj";

        private void Update()
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            int mask = 1 << LayerMask.NameToLayer(excluseLayerMask) | LayerMaskInteract.value;
            if (Physics.Raycast(transform.position,fwd, out hit,rayLenght,mask))
            {
                if(hit.collider.CompareTag(interactableTag)){
                    if (!doOnce)
                    {
                        raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                        CrossHairChange(true);
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

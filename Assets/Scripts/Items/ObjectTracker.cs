using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public GameObject targetObject; // Assign the target object in the Unity editor

    private void Update()
    {
        // Update the position and rotation of this game object to match the target object
        this.transform.position = targetObject.transform.position;
        // transform.rotation = targetObject.transform.rotation;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugDrawLine : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 50);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    Camera main;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        main= Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = main.transform.position;
        ray.direction = main.transform.forward;
        Physics.Raycast(ray, out hit);
        transform.position = hit.point;
        
    }
}

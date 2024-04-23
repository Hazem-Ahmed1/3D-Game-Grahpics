using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Animations.Rigging;

public class AimStateManager : MonoBehaviour
{
    public float turnSpeed = 15;
    Camera mainCamera;
    public float aimDuration = 0.3f;
    public Rig AimLayer;


    // Start is called before the first frame update
    void Start()
    {

        mainCamera = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,yawCamera,0),turnSpeed*Time.fixedDeltaTime);
    }

    private void Update()
    {
        AimLayer.weight = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] AudioSource Shoot;
    [HideInInspector]
    public RayCastWeapon weapon;
    public Transform crossHairTarget;
    public UnityEngine.Animations.Rigging.Rig HandIK;
    public Transform WeapoonParent;
    public Transform leftGrip;
    public Transform rightGrip;
    Animator animator;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        RayCastWeapon exisitingWeapon = GetComponentInChildren<RayCastWeapon>();
        if (exisitingWeapon)
        {
            Equip(exisitingWeapon);
        }
    }

    private void Update()
    {
        if (weapon)
        {
            if (Input.GetButtonDown("Fire1") && !weapon.isFiring)
            {
                Shoot.Play();
                weapon.StartFiring();
            }
            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
        }
        else
        {
            // Hammer Logic !
            HandIK.weight = 0f;
            animator.SetLayerWeight(2, 0.0f);
        }
    }

    public void Equip(RayCastWeapon Newweapon)
    {
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = Newweapon;
        weapon.RayCastDestination = crossHairTarget;
        HandIK.weight = 1.0f;
        weapon.transform.parent = WeapoonParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        animator.SetLayerWeight(2, 1.0f);

    }

}

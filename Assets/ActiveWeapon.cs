using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public RayCastWeapon weapon;
    public Transform crossHairTarget;
    public UnityEngine.Animations.Rigging.Rig HandIK;
    public Transform WeapoonParent;
    public Transform leftGrip;
    public Transform rightGrip;
    Animator animator;
    AnimatorOverrideController overrideController;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

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
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.StartFiring();
            }
            if (weapon.isFiring)
            {
                weapon.UpdateFiring(Time.deltaTime);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
            }
        }
        else
        {
            // Hammer Logic !
            HandIK.weight = 0f;
            animator.SetLayerWeight(2,0.0f);
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

        Invoke(nameof(SetAnimation), 0.001f);


    }

    public void SetAnimation()
    {

        overrideController["empty"] = weapon.weaponAnimation;
    }

    [ContextMenu("Save Weapon pose")]
    public void SaveWepaonPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(WeapoonParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(leftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(rightGrip.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(weapon.weaponAnimation);
        UnityEditor.AssetDatabase.SaveAssets();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPicker : MonoBehaviour
{

    public RayCastWeapon weaponFab;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeweapon  = other.GetComponent<ActiveWeapon>();
        if (activeweapon)
        {
             RayCastWeapon newWeapon = Instantiate(weaponFab);
             activeweapon.Equip(newWeapon);
        }
    }
}

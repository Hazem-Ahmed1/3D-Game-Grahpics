using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastWeapon : MonoBehaviour
{
    [SerializeField] public bool isFiring = false;

    public int FireRate = 25;

    public int maxAmmo = 1;
    public int currentAmmo;

    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect; 

    public Transform RaycastOrigin;
    public Transform RayCastDestination;

    Ray ray;
    RaycastHit hitInfo;

    float AccumulatedTime;
    public GameObject Bulletprefab;


    public AnimationClip weaponAnimation;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }
    public void StartFiring()
    {
        if(currentAmmo > 0)
        {
            isFiring = true;
            AccumulatedTime = 0.0f;
            FireBullet();
            GameObject bullet = Instantiate(Bulletprefab, RaycastOrigin.position, Quaternion.identity);
            BulletBehav bulletComponent = bullet.GetComponent<BulletBehav>();
            if (bulletComponent != null)
            {
                bulletComponent.MoveTowards(hitInfo.point);
            }
        }
    }

    public void UpdateFiring(float deltaTime)
    {
        AccumulatedTime += deltaTime;
        float fireInterval = 1.0f / FireRate;
        while(AccumulatedTime > 0.0f)
        {
            FireBullet();
            AccumulatedTime -= fireInterval;
        }
    }

    private void FireBullet()
    {
        muzzleFlash.Emit(1);

        ray.origin = RaycastOrigin.position;
        ray.direction = RayCastDestination.position - RaycastOrigin.position;

        if (Physics.Raycast(ray, out hitInfo))
        {
            // Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 5.0f);
            hitEffect.transform.position = hitInfo.point;
            hitEffect.transform.forward = hitInfo.normal;
            hitEffect.Emit(1);
        };

        currentAmmo--;
        if (currentAmmo <= 0)
        {
            Destroy(this.gameObject);
        }


    }

    public void StopFiring() { 
    
        isFiring= false;
    }
}

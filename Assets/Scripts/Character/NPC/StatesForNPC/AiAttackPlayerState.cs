using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAttackPlayerState : AiState
{
    private float lastShotTime = 0f;
    private float shotDelay = 2f; // 2-second delay between shots
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip audioClip_forShoot = null;
    public AiStateId GetId()
    {
        return AiStateId.AttackPlayer;
    }
    public void Enter(AiAgent agent)
    {
        agent.GetComponent<WeaponIK>().enabled = true;
        agent.navMeshAgent.stoppingDistance = 5f;
    }
    public void Update(AiAgent agent)
    {
        agent.navMeshAgent.destination = agent.PlayerTransform.transform.position;
        if (Time.time - lastShotTime >= shotDelay)
        {
            audioSource.clip = audioClip_forShoot;
            audioSource.Play();
            ShootAtPlayer(agent);
            lastShotTime = Time.time;
        }
    }

    public void Exit(AiAgent agent)
    {
        agent.GetComponent<WeaponIK>().enabled = false;
        agent.navMeshAgent.stoppingDistance = 0;
    }

    private void ShootAtPlayer(AiAgent agent)
    {
            var bullet = AiAgent.Instantiate(agent.BulletTornado, agent.bulletSpawnPoint.position, agent.bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = agent.bulletSpawnPoint.forward * agent.bulletSpeed;
    }
}

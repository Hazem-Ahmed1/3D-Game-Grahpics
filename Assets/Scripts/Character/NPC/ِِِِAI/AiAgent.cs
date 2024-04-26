using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class AiAgent : MonoBehaviour
{
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;

    public KeyDoorController keyDoorController;
    public KeyInventory keyInventory;
    public GameObject KeyFlag;
    public GameObject BodyLight;

    [HideInInspector] public GameObject PlayerTransform;
    [HideInInspector] public GameObject KeyTransform;
    [HideInInspector] public Transform FinalGoalTransform;
    [HideInInspector] public Animator animator;
    // public AiLocomotion aiLocomotion;
    public GameObject Weapon;
    // public GameObject NPC;
    public GameObject RigLayer;
    
    // public Animator animatorDozzy;
    public bool hasDoorLockedKey = false;
    public bool IsOpen = false;
    public bool dance = false;
    public bool Blind = false;
    private float distancePlayer;

    // for weapon
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine = new AiStateMachine(this);
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        FinalGoalTransform = GameObject.Find("LockedDoor").transform;
        KeyTransform = GameObject.Find("KeyDoor");

        // Create instance from this state
        stateMachine.RegisterState(new AiGetKeyState());
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiGoToFinalGoalState());
        stateMachine.RegisterState(new AiDanceState());
        stateMachine.RegisterState(new AiBlindState());
        stateMachine.RegisterState(new AiFreezeState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.RegisterState(new AiStealState());
        // stateMachine.RegisterState(new AiDeathState());
        initialState = AiStateId.goToKey;
        stateMachine.changeState(initialState);
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        KeyFlag.active = (hasDoorLockedKey)? true : false;
        BodyLight.active = (hasDoorLockedKey)? true : false;
        distancePlayer = Vector3.Distance(PlayerTransform.transform.position, this.gameObject.transform.position);
        float layerWeight = PlayerTransform.GetComponentInChildren<Animator>().GetLayerWeight(3);

        if (!hasDoorLockedKey && KeyTransform != null)
        {
            initialState = AiStateId.goToKey;
        }
        if (hasDoorLockedKey)
        {
            initialState = AiStateId.goToFinalGoal;
        }
        if (distancePlayer <= 10f && !hasDoorLockedKey && !dance && !Blind)
        {
            initialState = AiStateId.AttackPlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer >= 3f )
        {
            initialState = AiStateId.ChasePlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer < 5f && dance)
        {
            initialState = AiStateId.StealKey;
        }
        stateMachine.changeState(initialState);
        stateMachine.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("KeyDoor"))
        {
            hasDoorLockedKey = true;
            // keyInventory.hasDoorLockedKey = false;
            initialState = AiStateId.goToFinalGoal;
            stateMachine.changeState(initialState);
            Destroy(other.gameObject);
            navMeshAgent.stoppingDistance = 0;
        }
        else if (other.gameObject.CompareTag("DoorGoal") && hasDoorLockedKey && !IsOpen)
        {
            // print("Bedo");
            keyDoorController.OpenDoor();
            // keyInventory.hasDoorLockedKey = true;
            navMeshAgent.stoppingDistance = 1.5f;
            initialState = AiStateId.ChasePlayer;
            stateMachine.changeState(initialState);
            Physics.IgnoreLayerCollision(3,7);
            IsOpen = true;
        }
        else if (other.gameObject.CompareTag("Bullet_Dancing"))
        {
            initialState = AiStateId.Dance;
            stateMachine.changeState(initialState);
        }
        else if (other.gameObject.CompareTag("Blind_Bullet"))
        {
            // Debug.Log("Bedo");
            initialState = AiStateId.Blind;
            stateMachine.changeState(initialState);
        }
        else if (other.gameObject.CompareTag("BulletTornado"))
        {
            Debug.Log("Bedo");
            initialState = AiStateId.Freeze;
            stateMachine.changeState(initialState);
        }
    }
    // IEnumerator DestroyAfterDelay(float delay)
    // {
    //     navMeshAgent.speed = 0;
    //     yield return new WaitForSeconds(delay);
    //     // navMeshAgent.speed = 7;
    // }
}
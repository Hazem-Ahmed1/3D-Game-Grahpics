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
    int scoreNPC = 0;

    public KeyDoorController keyDoorController;
    public DoorController doorController;
    public KeyInventory keyInventory;
    public GameObject KeyFlag;
    public GameObject BodyLight;

    [HideInInspector] public GameObject PlayerTransform;
    [HideInInspector] public GameObject KeyTransform;
    [HideInInspector] public Transform FinalGoalTransform;
    [HideInInspector] public GameObject FinalDoorTransform;
    [HideInInspector] public Transform GoalTransform;
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
    // public bool freeze = false;
    public bool snatcher = false;
    public bool getGoal = true;
    private float distancePlayer;

    // for weapon
    public Transform bulletSpawnPoint;
    public GameObject BulletTornado;
    public float bulletSpeed = 10;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine = new AiStateMachine(this);
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        FinalDoorTransform = GameObject.Find("LockedDoor");
        KeyTransform = GameObject.Find("KeyDoor");
        FinalGoalTransform = GameObject.Find("FinalGoal").transform;
        GoalTransform = GameObject.Find("Goal").transform;

        // Create instance from this state
        stateMachine.RegisterState(new AiGetKeyState());
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiGoToFinalGoalState());
        stateMachine.RegisterState(new AiGotoDoorGoal());
        stateMachine.RegisterState(new AiDanceState());
        stateMachine.RegisterState(new AiBlindState());
        stateMachine.RegisterState(new AiFreezeState());
        stateMachine.RegisterState(new AiAttackPlayerState());
        stateMachine.RegisterState(new AiStealState());
        // stateMachine.RegisterState(new AiDeathState());
        initialState = AiStateId.goToKey;
        stateMachine.changeState(initialState);
        KeyFlag.active = hasDoorLockedKey? true : false;
        BodyLight.active = hasDoorLockedKey? true : false;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if(GameObject.Find("FinalGoal") != null){
            FinalGoalTransform = GameObject.Find("FinalGoal").transform;
        }
        if(GameObject.Find("KeyDoor") != null){
            KeyTransform = GameObject.Find("KeyDoor");
        }
        if(GameObject.Find("Goal") != null){
            GoalTransform = GameObject.Find("Goal").transform;
        }
        KeyFlag.active = hasDoorLockedKey? true : false;
        BodyLight.active = hasDoorLockedKey? true : false;
        distancePlayer = Vector3.Distance(PlayerTransform.transform.position, this.gameObject.transform.position);
        float layerWeight = PlayerTransform.GetComponentInChildren<Animator>().GetLayerWeight(3);

        if (!hasDoorLockedKey && KeyTransform != null && !snatcher)
        {
            initialState = AiStateId.goToKey;
        }
        if (hasDoorLockedKey && !snatcher)
        {
            initialState = AiStateId.goToFinalGoal;
            // getGoal = true;
        }
        if (distancePlayer <= 10f && !hasDoorLockedKey && !dance && !Blind && !snatcher)
        {
            initialState = AiStateId.AttackPlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer >= 3f && !snatcher)
        {
            initialState = AiStateId.ChasePlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer < 5f && dance && !snatcher)
        {
            initialState = AiStateId.StealKey;
        }
        if (snatcher)
        {
            initialState = AiStateId.goToFinalGoal;
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
            initialState = AiStateId.goToDoorGoal;
            stateMachine.changeState(initialState);
            Destroy(other.gameObject);
            navMeshAgent.stoppingDistance = 0;
        }
        else if (other.gameObject.name.Equals("Door"))
        {
            Debug.Log("Bedo");
            doorController.PlayAnimation();
            // keyInventory.hasDoorLockedKey = true;
            // navMeshAgent.stoppingDistance = 1.5f;
            // initialState = AiStateId.ChasePlayer;
            // stateMachine.changeState(initialState);
            // Physics.IgnoreLayerCollision(3,7);
            IsOpen = true;
        }
        else if (other.gameObject.name.Equals("FinalGoal"))
        {
            // print("Bedo");
            // doorController.PlayAnimation();
            // keyInventory.hasDoorLockedKey = true;
            // navMeshAgent.stoppingDistance = 1.5f;
            // initialState = AiStateId.ChasePlayer;
            // stateMachine.changeState(initialState);
            // Physics.IgnoreLayerCollision(3,7);
            // IsOpen = true;
            Destroy(other.gameObject);
            scoreNPC++;
            Debug.Log(scoreNPC);
        }
        else if (other.gameObject.name.Equals("Goal"))
        {
            // getGoal = false;
            Destroy(other.gameObject);
            scoreNPC++;
            Debug.Log(scoreNPC);
        }
        else if (other.gameObject.CompareTag("DoorGoal") && hasDoorLockedKey && !IsOpen)
        {
            // print("Bedo");
            keyDoorController.OpenDoor();
            // keyInventory.hasDoorLockedKey = true;
            getGoal = true;
            navMeshAgent.stoppingDistance = 0f;
            // initialState = AiStateId.goToFinalGoal;
            // stateMachine.changeState(initialState);
            // Physics.IgnoreLayerCollision(3,7);
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
}
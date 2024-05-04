using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class AiAgent : MonoBehaviour
{
    [SerializeField] public Image UIFill;
    public AiStateMachine stateMachine;
    public AiStateId initialState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public static int scoreNPC = 0;

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
    public treasureSpowner treasureSpowner;
    public TextMeshProUGUI points;
    public GameObject Weapon;
    public GameObject RigLayer;
    public bool hasDoorLockedKey = false;
    public bool IsOpen = false;
    public bool dance = false;
    public bool Blind = false;
    public bool snatcher = false;
    public bool getGoal = true;
    private float distancePlayer;

    // for weapon
    public Transform bulletSpawnPoint;
    public GameObject BulletTornado;
    public float bulletSpeed = 10;

    [SerializeField] public AudioSource audioSource = null;
    public AudioClip audioClip_ForPickItems = null;
    [SerializeField] public AudioClip audioClip_forShoot = null;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine = new AiStateMachine(this);
        PlayerTransform = GameObject.FindGameObjectWithTag("Player");
        // FinalDoorTransform = GameObject.Find("LockedDoor");
        KeyTransform = GameObject.Find("KeyDoor(Clone)");
        // FinalGoalTransform = GameObject.Find("FinalGoal(Clone)").transform;
        // GoalTransform = GameObject.Find("Goal").transform;

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

        initialState = AiStateId.goToKey;
        stateMachine.changeState(initialState);
        KeyFlag.active = hasDoorLockedKey? true : false;
        BodyLight.active = hasDoorLockedKey? true : false;
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        scoreNPC = 0;
        points.text = "X" + scoreNPC.ToString();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        if(GameObject.Find("FinalGoal(Clone)") != null){
            FinalGoalTransform = GameObject.Find("FinalGoal(Clone)").transform;
        }
        if(GameObject.Find("KeyDoor(Clone)") != null){
            // Debug.Log("88888");
            KeyTransform = GameObject.Find("KeyDoor(Clone)");
        }
        if(GameObject.Find("Goal(Clone)") != null){
            GoalTransform = GameObject.Find("Goal(Clone)").transform;
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
            navMeshAgent.stoppingDistance = 0;
            initialState = AiStateId.goToFinalGoal;
        }
        if (distancePlayer <= 10f && !hasDoorLockedKey && !dance && !Blind && !snatcher)
        {
            initialState = AiStateId.AttackPlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer >= 3f && !snatcher)
        {
            initialState = AiStateId.ChasePlayer;
        }
        if (keyInventory.hasDoorLockedKey && distancePlayer < 7f && !dance && !snatcher &&
        (PlayerTransform.GetComponentInChildren<Animator>().GetLayerWeight(3) == 1 || 
        PlayerTransform.GetComponent<MovementStateManager>().isRandomMovementActive))
        {
            navMeshAgent.stoppingDistance = 3;
            initialState = AiStateId.StealKey;
        }
        if (snatcher)
        {
            initialState = AiStateId.goToFinalGoal;
        }
        stateMachine.changeState(initialState);
        stateMachine.Update();
        if (navMeshAgent.hasPath)
        {
            animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        }else{
            animator.SetFloat("Speed",0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("KeyDoor(Clone)"))
        {
            audioSource.clip = audioClip_ForPickItems;
            audioSource.Play();
            hasDoorLockedKey = true;
            initialState = AiStateId.goToFinalGoal;
            stateMachine.changeState(initialState);
            Destroy(other.gameObject);
            navMeshAgent.stoppingDistance = 0;
        }
        else if (other.gameObject.name.Equals("Door"))
        {
            doorController.PlayAnimation();
            IsOpen = true;
        }
        else if (other.gameObject.name.Equals("FinalGoal(Clone)"))
        {
            audioSource.clip = audioClip_ForPickItems;
            audioSource.Play();
            Destroy(other.gameObject);
            scoreNPC++;
            points.text = "X" + scoreNPC.ToString();
            // Debug.Log(scoreNPC);
            treasureSpowner.SetupTreasureAndFlag();
        }
        else if (other.gameObject.name.Equals("Goal(Clone)"))
        {
            audioSource.clip = audioClip_ForPickItems;
            audioSource.Play();
            // Debug.Log("yarabb");
            Destroy(other.gameObject);
            scoreNPC++;
            points.text = "X" + scoreNPC.ToString();
            treasureSpowner.SetupTreasureAndFlag();
            // Debug.Log(scoreNPC);
        }
        else if (other.gameObject.CompareTag("DoorGoal") && hasDoorLockedKey && !IsOpen)
        {
            keyDoorController.OpenDoor();
            getGoal = true;
            navMeshAgent.stoppingDistance = 0f;
            IsOpen = true;
        }
        else if (other.gameObject.CompareTag("Bullet_Dancing"))
        {
            // Debug.Log("llll");
            initialState = AiStateId.Dance;
            stateMachine.changeState(initialState);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Blind_Bullet"))
        {
            initialState = AiStateId.Blind;
            stateMachine.changeState(initialState);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("BulletTornado"))
        {
            initialState = AiStateId.Freeze;
            stateMachine.changeState(initialState);
            Destroy(other.gameObject);
        }
    }
}
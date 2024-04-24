using System.Collections;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    public float currentMovementSpeed;
    public float walkSpeed = 3, walkBackSpeed = 2;
    public float runSpeed = 7, runBackSpeed = 5;

    [HideInInspector] public float hzInput, vInput;
    [HideInInspector] public Vector3 dir;

    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundYoffset;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;
    Vector3 spherePos;
    CharacterController characterController;

    MovementBaseState currentState;

    public IdleState idle = new IdleState();
    public RunState run = new RunState();
    public WalkState walk = new WalkState();

    [HideInInspector] public Animator anim;

    public bool isRandomMovementActive = false;

    // make it static it may works with out  references 
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        SwitchState(idle);
    }

    void Update()
    {
        if (!isRandomMovementActive)
        {
            GetDirectionAndMove();
            currentState.UpdateState(this);
        }
        else
        {
            RandomMovement();
        }

        Gravity();
        anim.SetFloat("hInput", hzInput);
        anim.SetFloat("vInput", vInput);
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartRandomMovement();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            EndRandomMovement();
        }

    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        characterController.Move(dir.normalized * currentMovementSpeed * Time.deltaTime);
    }

    bool IsGround()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYoffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, characterController.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if (!IsGround()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y > 0) velocity.y = -2;
        characterController.Move(velocity * Time.deltaTime);
    }

    public void StartRandomMovement()
    {
        isRandomMovementActive = true;
    }

    public void EndRandomMovement()
    {
        isRandomMovementActive = false;
    }

    void RandomMovement()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        Vector3 randomDirection = new Vector3(randomX, 0f, randomZ).normalized;
        float randomSpeed = Random.Range(0.5f, 1f);
        Vector3 moveVector = randomDirection * currentMovementSpeed * randomSpeed * Time.deltaTime;
        characterController.Move(moveVector);
    }
}

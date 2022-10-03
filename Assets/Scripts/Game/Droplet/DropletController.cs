using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(DropletMotor))]
public class DropletController : MonoBehaviour
{
    #region Fields

    [Header("Player Input:")]
    public bool jumpInput = false;
    public bool dashInput = false;
    public bool dropInput = false;
    public float moveInput = 0;
    public bool createFollowerInput = false;

    [Header("Jump Settings:")]
    [Range(0, 3)]
    [SerializeField] private readonly float JumpCooldown = 1f;
    private bool canJump = true;

    [Header("Stick to Ceiling Settings:")]
    [SerializeField]
    private float _stickyTime = 1;

    [Header("Enviroment Input")]
    public bool isOnFloor = false;

    [SerializeField]
    [Range(1,5)]
    private float _normalGravity = 3.5f;

    [Header("Drop Follower")]
    [SerializeField]
    private GameObject _dropFollowerPrefab;
    [SerializeField]
    private int _createFollowerCost = 10;

    [HideInInspector] 
    public bool canMove = true;

    private DropletMotor motor;
    private DropletHealth health;
    private DropletContamination contamination;
    private DropletGroundDetector groundDetector;
    private DropletGFX gfx;
    private Rigidbody2D rigidBody2D;
    private DropletFollow dropletFollow;
    private DropletScore dropletScore;

    #endregion


    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<DropletMotor>();
        health = GetComponent<DropletHealth>();
        contamination = GetComponent<DropletContamination>();
        groundDetector = GetComponent<DropletGroundDetector>();
        gfx = GetComponent<DropletGFX>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        dropletFollow = GetComponent<DropletFollow>();
        dropletScore = GetComponent<DropletScore>();

        Invoke("AddListeners",1);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGraphics();

        InputToAction();
    }

    private void FixedUpdate() 
    {
        Move();
    }

    #endregion


    #region Methods

    private void AddDrop(){
        if(health.currentHealth>_createFollowerCost){
            health.LoseHealth(_createFollowerCost);
            dropletFollow.AddDrop(_dropFollowerPrefab);
        }
    }

    private void UpdateGraphics()
    {
        gfx.RotateDroplet(groundDetector.NormalOfNearestGround * -1);
        gfx.SetIsOnGround(isOnFloor || groundDetector.IsTouchingCeiling);
        gfx.SetIsMoving(System.Math.Abs(rigidBody2D.velocity.y) <= 10);

        ChangeGravity();
    }

    private void ChangeGravity()
    {
        if (groundDetector.IsTouchingCeiling)
        {
            StartCoroutine("StickToCeilingCoroutine");
        }
        else if (isOnFloor)
        {
            rigidBody2D.gravityScale = 0.1f;
        }
        else
        {
            rigidBody2D.gravityScale = _normalGravity;
        }
    }

    private void AddListeners()
    {
        contamination.OnFullContaminated.AddListener(Die);
        health.OnNoHealth.AddListener(Die);
    }

    private void InputToAction() {
        if (jumpInput) Jump();
        if (createFollowerInput) AddDrop();
    }

    private void Move() {
        if (canMove){
            if(groundDetector.IsTouchingCeiling){
                motor.Move(moveInput, normalToGround: groundDetector.NormalOfNearestGround * -1);
            }
            else{
                motor.Move(moveInput, normalToGround: groundDetector.NormalOfNearestGround);
            }
        }
        gfx.Move(moveInput);
    }

    private void Jump() {
        if (canJump && isOnFloor) {
            gfx.Jump();
            StartCoroutine("JumpCoroutine");
        }
    }

    private void Die(){
        Debug.Log("I dieed");
    }

    #endregion


    #region Coroutines

    private IEnumerator JumpCoroutine() {
        canJump = false;
        yield return new WaitForSeconds(0.2f);
        motor.Jump(motor.JumpForce);
        yield return new WaitForSeconds(JumpCooldown);
        canJump = true;
    }

    private IEnumerator StickToCeilingCoroutine(){
        rigidBody2D.gravityScale = 0;
        yield return new WaitForSeconds(_stickyTime);
        rigidBody2D.gravityScale = _normalGravity;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.transform.tag)
        {
            case "Enemy":
                health.LoseHealth(5);
                break;
            case "ContaminationHazard":
                contamination.AddContamination(5);
                break;
            case "Drop":
                health.GainHealth(10);
                Destroy(other.gameObject);
                break;
            case "WhiteFlower":
                dropletScore.whiteFlowersCount += 1;
                other.GetComponent<Flower>().Bloom(contamination.contaminationPercent >= contamination.contaminationToBeContaminated);
                break;
            case "YellowFlower":
                dropletScore.redFlowersCount += 1;
                other.GetComponent<Flower>().Bloom(contamination.contaminationPercent >= contamination.contaminationToBeContaminated);
                break;
            case "BlueFlower":
                dropletScore.blueFlowersCount +=1;
                other.GetComponent<Flower>().Bloom(contamination.contaminationPercent >= contamination.contaminationToBeContaminated);
                break;
        }
    }
}

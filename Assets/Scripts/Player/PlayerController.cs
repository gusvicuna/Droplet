using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    #region Fields

    [Header("Player Input:")]
    public bool jumpInput = false;
    public bool dashInput = false;
    public bool dropInput = false;
    public float moveInput = 0;

    [Header("Jump Settings:")]
    [Range(0, 3)]
    [SerializeField] private readonly float JumpCooldown = 1f;
    private bool canJump = true;

    [Header("Enviroment Input")]
    public bool isOnFloor = false;

    [SerializeField]
    [Range(1,5)]
    private float _normalGravity = 3.5f;

    [HideInInspector] public bool canMove = true;

    private PlayerMotor motor;
    private PlayerHealth health;
    private PlayerContamination contamination;
    private DropletGroundDetector groundDetector;
    private DropletGFX gfx;
    private Rigidbody2D rigidBody2D;

    #endregion


    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        health = GetComponent<PlayerHealth>();
        contamination = GetComponent<PlayerContamination>();
        groundDetector = GetComponent<DropletGroundDetector>();
        gfx = GetComponent<DropletGFX>();
        rigidBody2D = GetComponent<Rigidbody2D>();

        Invoke("AddListeners",1);
    }

    // Update is called once per frame
    void Update()
    {
        gfx.RotateDroplet(groundDetector.NormalOfNearestGround * -1);

        GroundDetection();

        InputToAction();
    }

    private void GroundDetection()
    {
        if(groundDetector.IsTouchingGround){
            rigidBody2D.gravityScale = 1;
        }
        else{
            rigidBody2D.gravityScale = _normalGravity;
        }
    }

    private void FixedUpdate() 
    {
        Move();
    }

    #endregion


    #region Methods

    private void AddListeners()
    {
        contamination.OnFullContaminated.AddListener(Die);
        health.OnNoHealth.AddListener(Die);
    }

    private void InputToAction() {
        if (jumpInput) Jump();
    }
    private void Move() {
        if (canMove) motor.Move(moveInput, normalToGround: groundDetector.NormalOfNearestGround);
    }

    private void Jump() {
        if (canJump && isOnFloor) {
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
        motor.Jump(motor.JumpForce);
        yield return new WaitForSeconds(JumpCooldown);
        canJump = true;
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
        }
    }
}

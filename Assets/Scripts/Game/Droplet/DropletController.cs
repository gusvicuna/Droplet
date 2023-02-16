using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

[RequireComponent(typeof(DropletMotor))]
public class DropletController : MonoBehaviour
{
    #region Fields

    [Header("Player Input:")]
    public bool jumpInput = false;
    public bool dashInput = false;
    public bool dropInput = false;
    public bool solidifyInput = false;
    public bool liquifyInput = false;
    public bool vaporizeInput = false;
    public float moveInput = 0;
    public bool createFollowerInput = false;

    [Header("Jump Settings:")]
    [Range(0, 3)]
    [SerializeField] private readonly float JumpCooldown = 1f;
    private bool canJump = true;

    [Header("Stick to Ceiling Settings:")]
    public float stickyTime = 1;

    [Header("Enviroment Input")]
    public bool isOnFloor = false;
    [HideInInspector]
    public bool isInWater = false;

    [Range(1,5)]
    public float normalGravity = 3.5f;

    [Header("Drop Follower")]
    [SerializeField]
    private GameObject _dropFollowerPrefab;
    [SerializeField]
    private int _createFollowerCost = 10;

    [HideInInspector] 
    public bool canMove = true;
    [HideInInspector]
    public bool isContaminated = false;

    public GameObject vaporBody;
    public GameObject waterBody;
    public GameObject solidBody;

    [HideInInspector]
    public Health health;
    [HideInInspector]
    public Contamination contamination;
    [HideInInspector]
    public Temperature temperature;
    [HideInInspector]
    public DropletMotor motor;
    [HideInInspector]
    public DropletGroundDetector groundDetector;
    [HideInInspector]
    public DropletGFX gfx;
    [HideInInspector]
    public Rigidbody2D rigidBody2D;
    [HideInInspector]
    public DropletFollow dropletFollow;
    [HideInInspector]
    public DropletScore score;
    [HideInInspector]
    public DropletPhysicStateMachine physicStateMachine;
    private StatusChangeFeedbackText statusChangeText;

    public UnityEvent OnFinished;
    public UnityEvent Die;

    public UnityEvent OnGetComponents;

    #endregion


    #region Unity Methods

    private void Awake() {
        motor = GetComponent<DropletMotor>();
        health = GetComponent<Health>();
        temperature = GetComponent<Temperature>();
        contamination = GetComponent<Contamination>();
        groundDetector = GetComponent<DropletGroundDetector>();
        gfx = GetComponent<DropletGFX>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        dropletFollow = GetComponent<DropletFollow>();
        score = GetComponent<DropletScore>();
        statusChangeText = GetComponentInChildren<StatusChangeFeedbackText>();

        physicStateMachine = new DropletPhysicStateMachine(this);
        physicStateMachine.Initialize(physicStateMachine.liquidState);
    }
    // Start is called before the first frame update
    void Start()
    {
        OnGetComponents.Invoke();

        AddListeners();
    }

    // Update is called once per frame
    void Update()
    {
        physicStateMachine.Update();
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
        if(health.CurrentHealth>_createFollowerCost){
            health.Decrement(_createFollowerCost);
            dropletFollow.AddDrop(_dropFollowerPrefab);
        }
    }

    private void OnContaminated(){
        isContaminated = true;
        motor.normalMaxSpeed = motor.normalMaxSpeed * motor.contaminationSpeedMultiplier;
        gfx.OnContaminated();
    }

    private void UpdateGraphics()
    {
        gfx.RotateDroplet(groundDetector.NormalOfNearestGround * -1);
        gfx.SetIsOnGround(isOnFloor || groundDetector.IsTouchingCeiling);
        gfx.SetIsMoving(System.Math.Abs(rigidBody2D.velocity.y) <= 10);
    }

    private void AddListeners()
    {
        contamination.ContaminationAtFull += OnDeath;
        contamination.ContaminationAtHalf += OnContaminated;
        health.HealthIsZero += OnDeath;
    }

    private void InputToAction() {
        if (jumpInput) Jump();
        if (createFollowerInput) AddDrop();
        if (vaporizeInput) Vaporize();
        if (liquifyInput) Liquify();
        if (solidifyInput) Solidify();
    }

    private void Move() {
        if (canMove){
            if(isOnFloor){
                motor.Move(moveInput, groundDetector.NormalOfNearestGround, physicStateMachine.CurrentState != physicStateMachine.gasState);
            }
            else{
                motor.Move(moveInput, Vector2.up, physicStateMachine.CurrentState != physicStateMachine.gasState);
            }
        }
        gfx.Move(moveInput);
    }

    private void Jump() {
        if (canJump) {
            if(physicStateMachine.gasState == physicStateMachine.CurrentState || isOnFloor){
                gfx.Jump();
                StartCoroutine(JumpCoroutine());
            }
        }
    }

    private void Vaporize(){
        temperature.CurrentTemperature = 110;
    }
    private void Liquify(){
        temperature.CurrentTemperature = 30;
    }
    private void Solidify(){
        temperature.CurrentTemperature = -10;
    }

    private void OnDeath(){
        score.deathCounts +=1;
        Die.Invoke();
    }

    private void FinishLevel()
    {
        score.CalculateScore(health.CurrentHealth);
        OnFinished.Invoke();
    }

    public void LoadPlayerData(PlayerData playerData, int level){
        Debug.Log(playerData.ToString());
        score.LoadPlayerData(playerData, level);
    }

    public void AddTemperature(int deltaTemperature){
        temperature.Increase(deltaTemperature);
    }

    public void StartStickCoroutine(){
        StartCoroutine(StickToCeilingCoroutine());
    }

    #endregion


    #region Coroutines

    private IEnumerator JumpCoroutine() {
        canJump = false;
        yield return new WaitForSeconds(0.2f);
        if(physicStateMachine.CurrentState == physicStateMachine.gasState){
            motor.Jump(Vector2.down * motor.JumpForce * 0.2f);
        }
        else{
            motor.Jump(Vector2.up * motor.JumpForce);
        }
        yield return new WaitForSeconds(JumpCooldown);
        canJump = true;
    }

    public IEnumerator StickToCeilingCoroutine(){
        rigidBody2D.gravityScale = -0.1f;
        yield return new WaitForSeconds(stickyTime);
        rigidBody2D.gravityScale = normalGravity;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.transform.tag)
        {
            case "Drop":
                health.Increment(10);
                Destroy(other.gameObject);
                break;

            case "WhiteFlower":
                score.whiteFlowersCount += 1;
                if(isContaminated){
                    score.LosePoints(score.whiteFlowerScore);
                    statusChangeText.ShowText(-score.whiteFlowerScore, "score");
                }
                else{
                    score.WinPoints(score.whiteFlowerScore);
                    statusChangeText.ShowText(score.whiteFlowerScore, "score");
                }
                other.GetComponent<Flower>().Bloom(isContaminated);
                break;

            case "YellowFlower":
                score.yellowFlowersCount += 1;
                if(isContaminated){
                    score.LosePoints(score.yellowFlowerScore);
                    statusChangeText.ShowText(-score.yellowFlowerScore, "score");
                }
                else{
                    score.WinPoints(score.yellowFlowerScore);
                    statusChangeText.ShowText(score.yellowFlowerScore, "score");
                }
                other.GetComponent<Flower>().Bloom(isContaminated);
                break;

            case "BlueFlower":
                score.blueFlowersCount +=1;
                if(isContaminated){
                    score.LosePoints(score.blueFlowerScore);
                    statusChangeText.ShowText(-score.blueFlowerScore, "score");
                }
                else{
                    score.WinPoints(score.blueFlowerScore);
                    statusChangeText.ShowText(score.blueFlowerScore, "score");
                }
                other.GetComponent<Flower>().Bloom(isContaminated);
                break;

            case "Water":
                isInWater = true;
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, rigidBody2D.velocity.y * 0.1f);
                break;

            case "Finish":
                FinishLevel();
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        switch(other.tag){
                case "ContaminationModifier":
                    ContaminationModifier contaminator = other.GetComponent<ContaminationModifier>();
                    contamination.AddContamination(contaminator.contaminationAmount);
                    break;

                case "TemperatureModifier":
                    TemperatureModifier temperatureModifier = other.GetComponent<TemperatureModifier>();
                    AddTemperature(temperatureModifier.deltaTemperature);
                    break;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        switch(other.tag){
            case "Water":
                isInWater = false;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch(other.gameObject.tag){
            case "Enemy":
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                health.Decrement(enemy.damage);
                break;
            case "PressurePlate":
                PressurePlate pressurePlate = other.gameObject.GetComponent<PressurePlate>();
                pressurePlate.Toggle(health.CurrentHealth);
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        switch(other.gameObject.tag){
            case "ContaminationHazard":
                ContaminationModifier contaminator = other.gameObject.GetComponent<ContaminationModifier>();
                contamination.AddContamination(contaminator.contaminationAmount);
                break;
            case "TemperatureModifier":
                TemperatureModifier temperatureModifier = other.gameObject.GetComponent<TemperatureModifier>();
                temperature.Increase(temperatureModifier.deltaTemperature);
                break;
        }
    }
}

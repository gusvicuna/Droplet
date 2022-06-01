using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    #region Constants

    #endregion


    #region Fields

    [Header("Player Input:")]
    public bool JumpInput = false;
    public bool DashInput = false;
    public bool DropInput = false;
    public float MoveInput = 0;

    [Header("Jump Settings:")]
    [Range(0, 3)]
    [SerializeField] private readonly float JumpCooldown = 1f;
    private bool CanJump = true;

    [HideInInspector] public bool CanMove = true;

    private PlayerMotor Motor;

    #endregion


    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        Motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        InputToAction();
    }

    private void FixedUpdate() 
    {
        Move();
    }

    #endregion


    #region Methods

    private void InputToAction() {
        if (JumpInput) Jump();
    }
    private void Move() {
        if (CanMove) Motor.MoveRight(MoveInput);
    }

    private void Jump() {
        if (CanJump) {
            StartCoroutine("JumpCoroutine");
        }
    }

    #endregion


    #region Coroutines

    private IEnumerator JumpCoroutine() {
        CanJump = false;
        Motor.Jump(Motor.JumpForce);
        yield return new WaitForSeconds(JumpCooldown);
        CanJump = true;
    }

    #endregion
}

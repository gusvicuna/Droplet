using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletMotor : MonoBehaviour
{
    #region Fields

    [Range(0, 50)]
    public int NormalMaxSpeed = 10;
    [HideInInspector] 
    public int aceleration = 25;

    [Header("Jump Settings:")]
    [Range(15, 25)]
    public float JumpForce = 18;

    [Header("Dash Settings:")]
    [SerializeField]
    [Range(10, 30)]
    private readonly int DashSpeedBonus = 14;
    public bool IsDashing = false;

    private Rigidbody2D rigidBody;

    #endregion


    #region Unity Methods
    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    #endregion


    #region Methods

    public void Move(float moveInput, Vector2 normalToGround) {
        Vector2 force = Quaternion.AngleAxis(90,Vector3.back) * normalToGround * moveInput * aceleration;
        if(System.Math.Abs(rigidBody.velocity.x) < NormalMaxSpeed){
            rigidBody.AddForce(force);
        }
    }

    public void Jump(float force) {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, force);
    }

    internal void Dash() {
        StartCoroutine("DashCoroutine");
    }

    public IEnumerator DashCoroutine() {
        IsDashing = true;
        ResetVerticalSpeed();
        ActivateGravity(false);
        aceleration = DashSpeedBonus;
        while (IsDashing) {
            yield return null;
        }
        ActivateGravity(true);
        aceleration = NormalMaxSpeed;
    }

    public void ResetVerticalSpeed() {
        rigidBody.velocity = new Vector2(0, 0);
    }


    public void ActivateGravity(bool activate) {
        if (activate) rigidBody.gravityScale = 4;
        else rigidBody.gravityScale = 0;
    }

    public void FlipGravity(bool flip) {
        if (!flip) rigidBody.gravityScale = 4;
        else rigidBody.gravityScale = -4;
    }

    #endregion
}

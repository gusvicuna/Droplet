using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    #region Fields

    [Range(0, 50)]
    public int NormalSpeed = 25;
    [HideInInspector] 
    public int Speed;

    [Header("Jump Settings:")]
    [Range(15, 25)]
    public float JumpForce = 18;

    [Header("Dash Settings:")]
    [SerializeField]
    [Range(10, 30)]
    private readonly int DashSpeedBonus = 14;
    public bool IsDashing = false;

    private Rigidbody2D RigidBody;

    #endregion


    #region Unity Methods
    private void Start() {
        RigidBody = GetComponent<Rigidbody2D>();
        Speed = NormalSpeed;
    }

    #endregion


    #region Methods

    public void Move(float moveInput, Vector2 normalToGround) {
        Vector2 force = Quaternion.AngleAxis(90,Vector3.back) * normalToGround * moveInput * Speed;
        RigidBody.AddForce(force);
    }

    public void Jump(float force) {
        RigidBody.velocity = new Vector2(RigidBody.velocity.x, force);
    }

    internal void Dash() {
        StartCoroutine("DashCoroutine");
    }

    public IEnumerator DashCoroutine() {
        IsDashing = true;
        ResetVerticalSpeed();
        ActivateGravity(false);
        Speed = DashSpeedBonus;
        while (IsDashing) {
            yield return null;
        }
        ActivateGravity(true);
        Speed = NormalSpeed;
    }

    public void ResetVerticalSpeed() {
        RigidBody.velocity = new Vector2(0, 0);
    }


    public void ActivateGravity(bool activate) {
        if (activate) RigidBody.gravityScale = 4;
        else RigidBody.gravityScale = 0;
    }

    public void FlipGravity(bool flip) {
        if (!flip) RigidBody.gravityScale = 4;
        else RigidBody.gravityScale = -4;
    }

    #endregion
}

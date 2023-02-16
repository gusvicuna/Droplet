using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropletMotor : MonoBehaviour
{
    #region Fields

    [Range(0, 50)]
    public float normalMaxSpeed = 10;
    [Range(0, 1)]
    public float contaminationSpeedMultiplier = 0.5f;
    [HideInInspector] 
    public float aceleration = 1;

    [Header("Jump Settings:")]
    [Range(15, 25)]
    public float JumpForce = 18;

    private Rigidbody2D rigidBody;

    #endregion


    #region Unity Methods
    private void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    #endregion


    #region Methods

    public void Move(float moveInput, Vector2 normalToGround, bool slide) {
        Vector2 direction = Quaternion.AngleAxis(90,Vector3.back) * normalToGround * moveInput;
        if(slide){
            if(System.Math.Abs(rigidBody.velocity.x) < normalMaxSpeed){
                rigidBody.AddForce(direction * aceleration);
            }
        }
        else{
            transform.Translate(direction * normalMaxSpeed * Time.deltaTime);
        }
    }

    public void Jump(Vector2 force) {
        rigidBody.velocity = force;
    }

    #endregion
}

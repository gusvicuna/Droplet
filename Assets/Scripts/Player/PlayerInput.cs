using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    #region Fields

    private PlayerController Controller;

    #endregion


    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        Controller.MoveInput = Input.GetAxisRaw("Horizontal");
        CheckDropInput();
        CheckJumpInput();
        CheckDashInput();
    }

    private void CheckDashInput()
    {
        if (Input.GetButton("Dash"))
        {
            Controller.DashInput = true;
        }
        else
        {
            Controller.DashInput = false;
        }
    }

    private void CheckJumpInput()
    {
        if (Input.GetButton("Jump"))
        {
            Controller.JumpInput = true;
        }
        else
        {
            Controller.JumpInput = false;
        }
    }

    private void CheckDropInput()
    {
        if (Input.GetButton("Drop"))
        {
            Controller.DropInput = true;
        }
        else
        {
            Controller.DropInput = false;
        }
    }

    #endregion
}

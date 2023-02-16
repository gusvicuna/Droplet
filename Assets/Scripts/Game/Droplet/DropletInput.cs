using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropletController))]
public class DropletInput : MonoBehaviour
{
    #region Fields

    private DropletController Controller;
    public bool isActive = false;

    #endregion


    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<DropletController>();
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        CheckMovementInput();
        CheckDropInput();
        CheckJumpInput();
        CheckDashInput();
        CheckCreateFollowerInput();
        CheckSolidifyInput();
        CheckLiquifyInput();
        CheckVaporizeInput();
    }

    private void CheckSolidifyInput()
    {
        if (Input.GetButtonDown("Solidify") && isActive)
        {
            Controller.solidifyInput = true;
        }
        else
        {
            Controller.solidifyInput = false;
        }
    }
        private void CheckLiquifyInput()
    {
        if (Input.GetButtonDown("Liquify") && isActive)
        {
            Controller.liquifyInput = true;
        }
        else
        {
            Controller.liquifyInput = false;
        }
    }
        private void CheckVaporizeInput()
    {
        if (Input.GetButtonDown("Vaporize") && isActive)
        {
            Controller.vaporizeInput = true;
        }
        else
        {
            Controller.vaporizeInput = false;
        }
    }

    private void CheckMovementInput()
    {
        if (isActive)
        {
            Controller.moveInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            Controller.moveInput = 0;
        }
    }

    private void CheckDashInput()
    {
        if (Input.GetButtonDown("Dash") && isActive)
        {
            Controller.dashInput = true;
        }
        else
        {
            Controller.dashInput = false;
        }
    }

    private void CheckJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isActive)
        {
            Controller.jumpInput = true;
        }
        else
        {
            Controller.jumpInput = false;
        }
    }

    private void CheckDropInput()
    {
        if (Input.GetButtonDown("Drop") && isActive)
        {
            Controller.dropInput = true;
        }
        else
        {
            Controller.dropInput = false;
        }
    }

    private void CheckCreateFollowerInput(){
        if(Input.GetButtonDown("Create Follower") && isActive){
            Controller.createFollowerInput = true;
        }
        else{
            Controller.createFollowerInput = false;
        }
    }

    #endregion
}

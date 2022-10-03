using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropletController))]
public class DropletInput : MonoBehaviour
{
    #region Fields

    private DropletController Controller;

    #endregion


    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<DropletController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        Controller.moveInput = Input.GetAxisRaw("Horizontal");
        CheckDropInput();
        CheckJumpInput();
        CheckDashInput();
        CheckCreateFollowerInput();
    }

    private void CheckDashInput()
    {
        if (Input.GetButtonDown("Dash"))
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
        if (Input.GetButtonDown("Jump"))
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
        if (Input.GetButtonDown("Drop"))
        {
            Controller.dropInput = true;
        }
        else
        {
            Controller.dropInput = false;
        }
    }

    private void CheckCreateFollowerInput(){
        if(Input.GetButtonDown("Create Follower")){
            Controller.createFollowerInput = true;
        }
        else{
            Controller.createFollowerInput = false;
        }
    }

    #endregion
}

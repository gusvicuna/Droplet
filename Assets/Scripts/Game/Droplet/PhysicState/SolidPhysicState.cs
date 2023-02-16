using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidPhysicState : IState
{
    private DropletController droplet;

    public SolidPhysicState(DropletController droplet)
    {
        this.droplet = droplet;
    }
    
    public void Enter()
    {
        droplet.solidBody.SetActive(true);
        droplet.gfx.Solidify();
        droplet.normalGravity = 3.5f;
    }

    public void Update()
    {
        if(droplet.temperature.CurrentTemperature > droplet.temperature.TemperatureToSolidify){
            droplet.physicStateMachine.TransitionTo(droplet.physicStateMachine.liquidState);
        }

        if(droplet.isInWater){
            droplet.rigidBody2D.gravityScale = -1f;
        }
        else{
            droplet.rigidBody2D.gravityScale = droplet.normalGravity;
        }

        if(droplet.isOnFloor){
            droplet.rigidBody2D.gravityScale = 0.1f;
        }
    }
    
    public void Exit()
    {
        droplet.solidBody.SetActive(false);
    }
}

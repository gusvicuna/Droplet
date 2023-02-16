using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPhysicState : IState
{
    private DropletController droplet;

    public GasPhysicState(DropletController droplet)
    {
        this.droplet = droplet;
    }
    
    public void Enter()
    {
        droplet.vaporBody.SetActive(true);
        droplet.gfx.Vaporize();
        droplet.normalGravity = -0.3f;
    }

    public void Update()
    {
        if(droplet.temperature.CurrentTemperature < droplet.temperature.TemperatureToVaporize){
            droplet.physicStateMachine.TransitionTo(droplet.physicStateMachine.liquidState);
        }

        if(droplet.isInWater){
            droplet.rigidBody2D.gravityScale = -3.5f;
        }
        else{
            droplet.rigidBody2D.gravityScale = droplet.normalGravity;
        }

        if(droplet.groundDetector.IsTouchingGround){
            droplet.AddTemperature(-5);
        }
    }
    
    public void Exit()
    {
        droplet.vaporBody.SetActive(false);
    }
}

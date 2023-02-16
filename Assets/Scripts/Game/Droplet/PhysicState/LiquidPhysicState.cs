using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPhysicState : IState
{
    private DropletController droplet;

    public LiquidPhysicState(DropletController droplet)
    {
        this.droplet = droplet;
    }
    
    public void Enter()
    {
        droplet.waterBody.SetActive(true);
        droplet.gfx.Liquify();
        droplet.normalGravity = 3.5f;
    }

    public void Update()
    {
        if(droplet.temperature.CurrentTemperature >= droplet.temperature.TemperatureToVaporize){
            droplet.physicStateMachine.TransitionTo(droplet.physicStateMachine.gasState);
        }
        else if(droplet.temperature.CurrentTemperature <= droplet.temperature.TemperatureToSolidify){
            droplet.physicStateMachine.TransitionTo(droplet.physicStateMachine.solidState);
        }

        if(droplet.isInWater){
            droplet.rigidBody2D.gravityScale = 0.1f;
        }
        else{
            droplet.rigidBody2D.gravityScale = droplet.normalGravity; 
        }

        if(droplet.isOnFloor){
            droplet.rigidBody2D.gravityScale = 0.1f;
        }
        else if(droplet.groundDetector.IsTouchingCeiling){
            droplet.StartStickCoroutine();
        }
    }
    
    public void Exit()
    {
        droplet.waterBody.SetActive(false);
    }
}

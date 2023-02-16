using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DropletPhysicStateMachine
{
    public event Action StateChanged;

    public IState CurrentState { get; private set; }

    public SolidPhysicState solidState;
    public LiquidPhysicState liquidState;
    public GasPhysicState gasState;

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public DropletPhysicStateMachine(DropletController player)
    {
        this.solidState = new SolidPhysicState(player);
        this.liquidState = new LiquidPhysicState(player);
        this.gasState = new GasPhysicState(player);
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();

        StateChanged?.Invoke();
    }

    public void Update()
    {
        CurrentState?.Update();
    }
}

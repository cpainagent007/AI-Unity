using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    Dictionary<string, AIState> states = new Dictionary<string, AIState>();

    public AIState CurrentState { get; private set; }

    public void AddState(AIState state)
    {
        if (states.ContainsKey(state.Name))
        {
            Debug.LogError($"State Machine already contains {state.Name}");
            return;
        }
        states[state.Name] = state;
    }

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void SetState<T>()
    {
        SetState(typeof(T).Name);
    }

    public void SetState(string name)
    {
        if(!states.ContainsKey(name))
        {
            Debug.LogError($"State Machine does not contain {name}");
            return;
        }

        var nextState = states[name];
        if (nextState == null || nextState == CurrentState)
        {
            return;
        }

        CurrentState?.OnExit();
        CurrentState = nextState;
        CurrentState.OnEnter();
    }
}

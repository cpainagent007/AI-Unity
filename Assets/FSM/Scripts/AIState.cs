using UnityEngine;

public abstract class AIState
{
    protected StateAgent agent;

    public AIState(StateAgent agent)
    {
        this.agent = agent;
    }

    public string Name => GetType().Name;

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

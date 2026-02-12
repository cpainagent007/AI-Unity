using UnityEngine;

public class AIDeathState : AIState
{
    public AIDeathState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.animator.SetTrigger("Death");
        GameObject.Destroy(agent.gameObject, 5.0f);
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}

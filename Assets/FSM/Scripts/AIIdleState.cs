using UnityEngine;

public class AIIdleState : AIState
{
    public AIIdleState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.timer = 2.0f;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (agent.timer <= 0)
        {
            agent.StateMachine.SetState<AIPatrolState>();
        }
    }
}

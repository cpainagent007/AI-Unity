using UnityEngine;

public class AIPatrolState : AIState
{
    public AIPatrolState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.Destination = NavNode.GetRandomNavNode().transform.position;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (agent.distanceToDestination <= 0.5f)
        {
            agent.StateMachine.SetState<AIIdleState>();
        }

        if (agent.enemy != null)
        {
            agent.StateMachine.SetState<AIChaseState>();
        }
    }
}

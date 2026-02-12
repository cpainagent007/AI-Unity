using UnityEngine;

public class AIChaseState : AIState
{
    public AIChaseState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.movement.maxSpeed *= 1.5f;
    }

    public override void OnExit()
    {
        agent.movement.maxSpeed /= 1.5f;
    }

    public override void OnUpdate()
    {
        if (agent.enemy != null)
        {
            agent.movement.Destination = agent.enemy.transform.position;
            if (agent.distanceToDestination <= 0.5f)
            {
                agent.StateMachine.SetState<AIAttackState>();
            }
        }
        else
        {
            agent.StateMachine.SetState<AIIdleState>();
        }
    }
}

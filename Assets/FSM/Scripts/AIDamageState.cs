using UnityEngine;

public class AIDamageState : AIState
{
    public AIDamageState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.animator.SetTrigger("Damage");
        agent.timer = 1.0f;
        agent.movement.Destination = agent.transform.position;
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

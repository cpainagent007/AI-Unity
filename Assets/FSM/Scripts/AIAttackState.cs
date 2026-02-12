using System.Security.Cryptography;
using UnityEngine;

public class AIAttackState : AIState
{
    public AIAttackState(StateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.animator.SetTrigger("Attack");
        agent.movement.Destination = agent.transform.position;
        agent.timer = 1.0f;

        Attack();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        if (agent.timer <= 0.0f)
        {
            agent.StateMachine.SetState<AIChaseState>();
        }
    }

    void Attack()
    {
        var colliders = Physics.OverlapSphere(agent.transform.position, agent.attackRange);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag(agent.tag)) continue;

            if (collider.gameObject.TryGetComponent<StateAgent>(out var stateAgent))
            {
                stateAgent.OnDamage(agent.attackDamage);
            }
        }
    }
}

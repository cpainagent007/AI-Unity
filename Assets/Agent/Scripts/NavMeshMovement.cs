using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovement : Movement
{
    NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override Vector3 Velocity
    {
        get
        {
            return navMeshAgent.velocity;
        }

        set
        {
            navMeshAgent.velocity = value;
        }
    }

    public override Vector3 Destination
    {
        get
        {
            return navMeshAgent.destination;
        }

        set
        {
            navMeshAgent.destination = value;
        }
    }

    private void Update()
    {
        navMeshAgent.speed = maxSpeed;
    }
}

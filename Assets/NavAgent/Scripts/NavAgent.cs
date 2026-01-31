using UnityEngine;

public class NavAgent : AIAgent
{
    [SerializeField] Movement movement;
    [SerializeField, Range(1, 20)] float rotationRate = 1;

    public NavNode TargetNode { get; set; } = null;

    void Start()
    {
        TargetNode = NavNode.GetNearestNavNode(transform.position);
    }

    void Update()
    {
        if (TargetNode != null)
        {
            Vector3 direction = TargetNode.transform.position - transform.position;
            Vector3 force = direction.normalized * movement.maxForce;

            movement.ApplyForce(force);
        }

        if (movement.Velocity.sqrMagnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(movement.Velocity);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,Time.deltaTime * rotationRate);
        }
    }

    public void OnEnterNavNode(NavNode navNode)
    {
        if (navNode == TargetNode)
        {
            TargetNode = navNode.Neighbors[Random.Range(0, navNode.Neighbors.Count)];
        }
    }
}

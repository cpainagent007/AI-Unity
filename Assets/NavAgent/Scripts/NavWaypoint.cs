using UnityEngine;

public class NavWaypoint : NavNode
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavAgent>(out NavAgent agent))
        {
            agent.TargetNode = neighbors[Random.Range(0, neighbors.Count)];
        }
    }
}

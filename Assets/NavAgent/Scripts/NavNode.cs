using UnityEngine;
using System.Collections.Generic;

public class NavNode : MonoBehaviour
{
    [SerializeField] protected List<NavNode> neighbors;

    public List<NavNode> Neighbors { get { return neighbors; } set { neighbors = value; } }
    public float Cost { get; set; } = 0;
    public NavNode PreviousNavNode { get; set; } = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavPathMovement>(out NavPathMovement navMovement))
        {
            navMovement.OnEnterNavNode(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent<NavPathMovement>(out NavPathMovement navMovement))
        {
            navMovement.OnEnterNavNode(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var neighbor in neighbors)
        {
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }

    #region helper_functions

    public static NavNode[] GetAllNavNodes()
    {
        return FindObjectsByType<NavNode>(FindObjectsSortMode.None);
    }

    public static NavNode GetRandomNavNode()
    {
        var navNodes = GetAllNavNodes();
        return (navNodes.Length == 0) ? null : navNodes[Random.Range(0, navNodes.Length)];
    }

    public static NavNode GetNearestNavNode(Vector3 position)
    {
        NavNode nearestNavNode = null;
        float nearestDistance = float.MaxValue;

        var navNodes = GetAllNavNodes();
        foreach (var navNode in navNodes)
        {
            float distance = Vector3.Distance(navNode.transform.position, position);
            if (distance < nearestDistance)
            {
                nearestNavNode = navNode;
                nearestDistance = distance;
            }
        }

        return nearestNavNode;
    }

    public static void ResetNavNodes()
    {
        var navNodes = GetAllNavNodes();
        foreach (var navNode in navNodes)
        {
            navNode.Cost = float.MaxValue;
            navNode.PreviousNavNode = null;
        }
    }

    public static void CreatePath(NavNode navNode, ref List<NavNode> path)
    {
        while(navNode != null)
        {
            path.Add(navNode);
            navNode = navNode.PreviousNavNode;
        }
        path.Reverse();
    }

    #endregion
}

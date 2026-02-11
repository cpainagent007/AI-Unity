using UnityEngine;
using System.Collections.Generic;

public class NavPath : MonoBehaviour
{
    List<NavNode> path = new List<NavNode>();

    public NavNode GeneratePath(Vector3 startPosition, Vector3 endPosition)
    {
        var startNode = NavNode.GetNearestNavNode(startPosition);
        var endNode = NavNode.GetNearestNavNode(endPosition);

        GeneratePath(startNode, endNode);

        return startNode;
    }

    public NavNode GeneratePath(NavNode startNode, NavNode endNode)
    {
        path.Clear();
        NavNode.ResetNavNodes();

        NavDijkstra.Generate(startNode, endNode, ref path);
        //NavAStar.Generate(startNode, endNode, ref path);

        return startNode;
    }

    public NavNode GetNextNavNode(NavNode navNode)
    {
        if (path.Count == 0) return null;

        int index = path.IndexOf(navNode);

        if (index == -1 || index + 1 >= path.Count) return null;

        return path[index + 1];
    }

    private void OnDrawGizmosSelected()
    {
        if (path.Count == 0) return;

        // Draw intermediate nodes (white)
        for (int i = 1; i < path.Count - 1; i++)
        {
            Gizmos.color = Utilities.white;
            Gizmos.DrawSphere(path[i].transform.position, 1);
        }

        // Draw start node (green) and end node (red)
        Gizmos.color = Utilities.green;
        Gizmos.DrawSphere(path[0].transform.position, 1);

        Gizmos.color = Utilities.red;
        Gizmos.DrawSphere(path[^1].transform.position, 1);
    }
}

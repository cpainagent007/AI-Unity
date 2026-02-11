using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public static class NavDijkstra
{ 
    public static bool Generate(NavNode startNode, NavNode endNode, ref List<NavNode> path)
    {
        // Initialize a priority queue to manage nodes by their cost
        // Nodes with lower costs will be processed first
        var nodes = new SimplePriorityQueue<NavNode>();

        // Initialize the start node with cost 0
        // All other nodes implicitly have infinite cost
        startNode.Cost = 0;

        nodes.Enqueue(startNode, startNode.Cost);

        // Process nodes until we find the end node or exhaust all options
        while (nodes.Count != 0)
        {
            // Get the node with smallest cost
            // This node is guaranteed to have the shortest path from start
            var currentNode = nodes.Dequeue();

            // If this is our target node, we've found the shortest path
            // Build the path and return success
            if (currentNode == endNode) {
                NavNode.CreatePath(endNode, ref path);
                return true;
            }
            

            // Check each connection from current node
            // This explores all possible paths one step further
            foreach (var neighbor in currentNode.Neighbors)
            {
                // Calculate total cost to reach this neighbor
                // Cost = (cost to current) + (distance from current to neighbor)
                float cost = currentNode.Cost + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);

                // If we found a shorter path to this neighbor
                // Update its cost and set current node as its predecessor
                if (cost < neighbor.Cost)
                {
                    neighbor.Cost = cost;

                    neighbor.PreviousNavNode = currentNode;

                    // Add/Update neighbor in priority queue
                    // It will be explored later based on its new priority
                    nodes.EnqueueWithoutDuplicates(neighbor, cost);
                }
            }
        }

        // No path found to end node
        return false;
    }
}

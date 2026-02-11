using UnityEngine;

[RequireComponent(typeof(NavPath))]
public class NavPathMovement : KinematicMovement
{
    NavPath navPath = null;
    public NavNode TargetNavNode { get; set; } = null;

    private void Awake()
    {
        navPath = GetComponent<NavPath>();
    }

    public override Vector3 Destination
    {
        get
        {
            // check if there is a target, if not return current position
            return (TargetNavNode != null) ?
             TargetNavNode.transform.position :
             transform.position;
        }
        set
        {
            TargetNavNode = navPath.GeneratePath(transform.position, value);
        }
    }

    void Update()
    {
        if (TargetNavNode != null)
        {
            // move towards target node
            Vector3 direction = TargetNavNode.transform.position - transform.position;
            Vector3 force = direction.normalized * maxForce;

            ApplyForce(force);
        }
        else
        {
            // no target, stop
            Velocity = Vector3.zero;
        }
    }

    public void OnEnterNavNode(NavNode navNode)
    {
        if (navNode == TargetNavNode)
        {
            // get next nav node in path, returns null if no next
            TargetNavNode = navPath.GetNextNavNode(navNode);
        }
    }
}
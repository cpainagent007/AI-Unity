using System.Drawing;
using UnityEngine;

public class AutonomousAgent : AIAgent
{
    [Header("Wander")]
    [SerializeField] float wanderRadius = 1;
    [SerializeField] float wanderDistance = 1;
    [SerializeField] float wanderDisplacement = 1;
    float wanderAngle = 0.0f;

    [SerializeField] Movement movement;
    [SerializeField] Perception seekPerception;
    [SerializeField] Perception fleePerception;

    void Start()
    {
        wanderAngle = Random.Range(0, 360);
    }
    void Update()
    {
        bool hasTarget = false;

        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                hasTarget = true;
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }

        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                hasTarget = true;
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);
            }
        }

        if (!hasTarget)
        {
            Vector3 force = Wander();
            movement.ApplyForce(force);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-15, -15, -15), new Vector3(15, 15, 15));

        if (movement.Velocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movement.Velocity, Vector3.up);
        }
    }

    Vector3 Seek(GameObject go)
    {
        Vector3 direction = go.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);
        return force;
    }

    Vector3 Flee(GameObject go)
    {
        Vector3 direction = transform.position - go.transform.position;
        Vector3 force = GetSteeringForce(direction);
        return force;
    }

    private Vector3 Wander()
    {

        // randomly adjust the wander angle within (+/-) displacement range 

        wanderAngle += Random.Range(-wanderDisplacement, wanderDisplacement);

        // calculate a point on the wander circle using the wander angle 

        Quaternion rotation = Quaternion.AngleAxis(wanderAngle, Vector3.up);

        Vector3 pointOnCircle = rotation * (Vector3.forward * wanderRadius);

        // project the wander circle in front of the agent 

        Vector3 circleCenter = movement.Velocity.normalized * wanderDistance;

        // steer toward the target point (circle center + point on circle) 

        Vector3 force = GetSteeringForce(circleCenter + pointOnCircle);

        Debug.DrawLine(transform.position, transform.position + circleCenter, UnityEngine.Color.blue);
        Debug.DrawLine(transform.position, transform.position + circleCenter + pointOnCircle, UnityEngine.Color.red);

        return force;
    }

    Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);
        return force;
    }
}

using UnityEngine;

public class StateAgent : AIAgent
{

    public Movement movement;
    public Perception perception;
    public Animator animator;

    [Header("Parameters")]
    [SerializeField] public float maxHealth = 100.0f;
    [SerializeField] public float attackRange = 2.0f;
    [SerializeField] public float attackDamage = 10.0f;
    public float timer;
    public float health;
    public float distanceToDestination;
    public AIAgent enemy;

    public AIStateMachine StateMachine { get; private set; } = new AIStateMachine();

    public Vector3 Destination
    {
        get { return movement.Destination; }
        set { movement.Destination = value; }
    }

    private void Start()
    {
        health = maxHealth;
        StateMachine.AddState(new AIAttackState(this));
        StateMachine.AddState(new AIChaseState(this));
        StateMachine.AddState(new AIDamageState(this));
        StateMachine.AddState(new AIDeathState(this));
        StateMachine.AddState(new AIIdleState(this));
        StateMachine.AddState(new AIPatrolState(this));

        StateMachine.SetState<AIIdleState>();
    }

    private void Update()
    {
        UpdateParameters();
        StateMachine.Update();
    }

    private void UpdateParameters()
    {
        //parameters
        timer -= Time.deltaTime;
        distanceToDestination = Vector3.Distance(transform.position, Destination);
        var gameObjects = perception.GetGameObjects();

        //enemies
        if (gameObjects.Length > 0)
        {
            gameObjects[0].TryGetComponent<AIAgent>(out enemy);
        }
        else
        {
            enemy = null;
        }
    }

    public void OnDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            StateMachine.SetState<AIDeathState>();
        }
        else
        {
            StateMachine.SetState<AIDamageState>();
        }
    }
}

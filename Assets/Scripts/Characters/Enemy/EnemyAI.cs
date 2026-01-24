using System;
using Move;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class EnemyAI : Enemy
{
    public event EventHandler OnEnemyAttack;
    public State startState;

    private MovableSprite movableSprite;

    private float roamingDistanceMax = 3f, roamingDistanceMin = 1f, roamingTimerMax = 3f;
    private float roamingTime, idleTime;
    private Vector3 roamPosition, startPosition;

    public State currentState;
    private NavMeshAgent navMeshAgent;

    private bool reachTheGoal;
    private Animator animator;

    public bool isChasingEnemy;
    private float chasingDistance = 2f;

    public bool isAttackingEnemy;
    private float attackingDistance = 1f, attackRate = 2f, nextAttackTime = 0f;

    private float nextCheckDirectionTime = 0f, checkDirectionDuration = 0.1f;
    private Vector3 lastPosition;

    public bool IsRunning => navMeshAgent.velocity != Vector3.zero;


    public enum State
    {
        Idle,
        Roaming,
        Chasing,
        Attacking
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        movableSprite = new MovableSprite();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = 0.7f;
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        currentState = startState;
    }

    private void Update()
    {
        StateHandler();
        MovementDirectionHandler();

        if (isHitting) StartCoroutine(ChangeColor());
        if (hearts <= 0f)
        {
            EnemyManager.Instance.LoseEnemy();
            Destroy(gameObject);
        }
        StartCoroutine(Hit());
    }

    private void StateHandler()
    {
        switch (currentState)
        {
            case State.Roaming:
                roamingTime -= Time.deltaTime;

                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }

                CheckCurrentState();
                break;

            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break;

            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break;

            default:
            case State.Idle:
                IdleTarget();
                idleTime += Time.deltaTime;

                if (idleTime > 1.5f)
                {
                    CheckCurrentState();
                    idleTime = 0f;
                }
                break;
        }
    }

    private void IdleTarget()
    {
        movableSprite.SetAnimator(animator);
    }

    private void Roaming()
    {
        animator.SetBool("Attack", false);

        startPosition = transform.position;
        roamPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(roamPosition);

        reachTheGoal = false;
    }

    private void AttackingTarget()
    {
        animator.SetBool("Attack", true);
        StartCoroutine(Hit());

        if (Time.time > nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);

            nextAttackTime = Time.time + attackRate;
        }
    }

    private void ChasingTarget()
    {
        animator.SetBool("Attack", false);

        navMeshAgent.SetDestination(Player.Instance.transform.position);
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Roaming;

        if (distanceToPlayer <= attackingDistance && isAttackingEnemy)
        {
            newState = State.Attacking;
        }

        else if (distanceToPlayer <= chasingDistance && isChasingEnemy)
        {
            newState = State.Chasing;
        }

        else if (currentState == State.Roaming && navMeshAgent.velocity == Vector3.zero)
        {
            newState = State.Idle;
        }

        if (newState != currentState)
        {
            if (newState == State.Chasing)
            {
                navMeshAgent.ResetPath();
            }
            else if (newState == State.Roaming)
            {
                roamingTime = 0f;
            }
            else if (newState == State.Attacking)
            {
                navMeshAgent.ResetPath();
            }

            currentState = newState;
        }
    }

    private void MovementDirectionHandler()
    {
        if (Time.time > nextCheckDirectionTime)
        {
            if (IsRunning && !reachTheGoal)
            {
                ChangeFacingDirection(lastPosition, transform.position);
                reachTheGoal = true;
            }
            else if (currentState == State.Attacking)
            {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
                reachTheGoal = true;
            }

            lastPosition = transform.position;
            nextCheckDirectionTime = Time.time + checkDirectionDuration;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startPosition + GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    private void LeftRight(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.x > targetPosition.x) movableSprite.SetDirection(false, true, false, false);
        else if (sourcePosition.x < targetPosition.x) movableSprite.SetDirection(true, false, false, false);
    }

    private void UpDown(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.y > targetPosition.y) movableSprite.SetDirection(false, false, true, false);
        else if (sourcePosition.y < targetPosition.y) movableSprite.SetDirection(false, false, false, true);
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            LeftRight(sourcePosition, targetPosition);
            UpDown(sourcePosition, targetPosition);
        }
        else
        {
            UpDown(sourcePosition, targetPosition);
            LeftRight(sourcePosition, targetPosition);
        }

        movableSprite.SetAnimator(animator);
    }
}

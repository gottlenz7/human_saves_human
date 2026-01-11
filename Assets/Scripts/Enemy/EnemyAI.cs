using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public event EventHandler OnEnenmyAttack;
    public State startState;

    private float roamingDistanceMax = 3f, roamingDistanceMin = 1f, roamingTimerMax = 3f;
    private float roamingTime, idleTime;
    private Vector3 roamPosition, startPosition;

    private State currentState;
    private NavMeshAgent navMeshAgent;

    private bool isRight, isDown, isUp, isLeft;
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
        animator.SetBool("isDown", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isLeft", false);
        animator.SetBool("isUp", false);
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

        if (Time.time > nextAttackTime)
        {
            OnEnenmyAttack?.Invoke(this, EventArgs.Empty);

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
            if (IsRunning)
            {
                ChangeFacingDirection(lastPosition, transform.position);
            }
            else if (currentState == State.Attacking)
            {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
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
        if (sourcePosition.x > targetPosition.x)
        {
            isRight = false;
            isLeft = true;
            isDown = false;
            isUp = false;
        }
        else if (sourcePosition.x < targetPosition.x)
        {
            isRight = true;
            isLeft = false;
            isDown = false;
            isUp = false;
        }
    }

    private void UpDown(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (sourcePosition.y > targetPosition.y)
        {
            isRight = false;
            isLeft = false;
            isDown = true;
            isUp = false;
        }
        else if (sourcePosition.y < targetPosition.y)
        {
            isRight = false;
            isLeft = false;
            isDown = false;
            isUp = true;
        }
    }

    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)
    {
        if (!reachTheGoal)
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
            reachTheGoal = true;
        }

        animator.SetBool("isDown", isDown);
        animator.SetBool("isRight", isRight);
        animator.SetBool("isLeft", isLeft);
        animator.SetBool("isUp", isUp);
    }
}

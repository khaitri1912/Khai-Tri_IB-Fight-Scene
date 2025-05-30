using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    List<Transform> enemyPoints = new List<Transform>();
    NavMeshAgent _agent;

    private float _Timer;

    Transform _player;
    Transform _ally;
    float _allyHealth;
    float _chaseRange = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer = 0;

        _player = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject allyObject = GameObject.FindGameObjectWithTag("Ally");

        if (allyObject != null)
        {
            _ally = allyObject.transform;
            _allyHealth = Ally.allyInstance.allyStats.health;
        }
        else
        {
            _ally = null;
            _allyHealth = -10;
        }
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 0.5f;

        GameObject go = GameObject.FindGameObjectWithTag("EnemyPoints");

        foreach (Transform t in go.transform)
        {
            enemyPoints.Add(t);
        }

        _agent.SetDestination(enemyPoints[Random.RandomRange(0, enemyPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer += Time.deltaTime;
        if (_Timer > 8)
        {
            animator.SetBool("isPatrolling", false);
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(enemyPoints[Random.RandomRange(0, enemyPoints.Count)].position);
        }
        

        Transform _closestTarget = GetClosestTarget(_player, _ally, animator.transform);

        float distanceToChaseTarget = Vector3.Distance(_closestTarget.position, animator.transform.position);
        
        if (distanceToChaseTarget < _chaseRange)
        {
            if (_allyHealth != -10)
            {
                if (Player.PlayerInstance.playerStats.health <= 0
                || Ally.allyInstance.allyStats.health <= 0)
                {
                    animator.SetBool("isChasing", false);
                }
                else
                {
                    animator.SetBool("isChasing", true);
                }
            }else 
            {
                if (Player.PlayerInstance.playerStats.health <= 0)
                {
                    animator.SetBool("isChasing", false);
                }
                else
                {
                    animator.SetBool("isChasing", true);
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
    }

    Transform GetClosestTarget(Transform _player,Transform _ally, Transform animatorTransform)
    {
        if (_ally == null)
        {
            return _player;
        }

        float distanceToPlayer = Vector3.Distance(_player.position, animatorTransform.position);
        float distanceToAlly = Vector3.Distance(_ally.position, animatorTransform.position);

        if (distanceToPlayer < distanceToAlly)
        {
            return _player;
        }
        else
        {
            Debug.Log(_ally.name);
            return _ally;
        }
    }
}

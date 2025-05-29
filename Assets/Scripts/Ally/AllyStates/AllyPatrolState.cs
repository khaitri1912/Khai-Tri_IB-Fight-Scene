using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyPatrolState : StateMachineBehaviour
{
    List<Transform> _allyPoints = new List<Transform>();
    NavMeshAgent _agent;

    private float _timer;

    Transform _enemy;
    float _chaseRange = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0;

        _enemy = GameObject.FindGameObjectWithTag("Enemy").transform;

        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 0.5f;

        GameObject go = GameObject.FindGameObjectWithTag("AllyPoints");

        foreach (Transform t in go.transform)
        {
            _allyPoints.Add(t);
        }

        _agent.SetDestination(_allyPoints[Random.RandomRange(0, _allyPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (_timer > 8)
        {
            animator.SetBool("isAllyPatrolling", false);
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(_allyPoints[Random.RandomRange(0, _allyPoints.Count)].position);
        }

        float disanceToChase = Vector3.Distance(_enemy.position, animator.transform.position);

        if (disanceToChase < _chaseRange)
        {
            if (Enemy.enemyInstance.enemyStats.health <= 0)
            {
                animator.SetBool("isAllyChasing", false);
            }
            else
            {
                animator.SetBool("isAllyChasing", true);
            }
        }

        if (_enemy == null)
        {
            animator.SetBool("isAllyVictory", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

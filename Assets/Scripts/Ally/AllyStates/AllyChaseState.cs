using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyChaseState : StateMachineBehaviour
{
    Transform _enemy;
    NavMeshAgent _agent;

    float _chaseRange = 2f;
    float _attackRange = 1.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 1.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_enemy.position);

        float distance = Vector3.Distance(_enemy.position, animator.transform.position);

        if (distance > _chaseRange)
        {
            animator.SetBool("isAllyChasing", false);
        }else if (distance < _chaseRange) 
        {
            if (Enemy.enemyInstance.enemyStats.health <= 0)
            {
                animator.SetBool("isAllyChasing", false);
            }
        }

        if (distance < _attackRange)
        {
            if (Enemy.enemyInstance.enemyStats.health <= 0)
            {
                animator.SetBool("isAllyAttacking", false);
            }
            else
            {
                animator.SetBool("isAllyAttacking", true);
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
        _agent.SetDestination(animator.transform.position);
    }
}

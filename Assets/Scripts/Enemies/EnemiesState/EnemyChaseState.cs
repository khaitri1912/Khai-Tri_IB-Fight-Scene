using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : StateMachineBehaviour
{
    Transform _player;
    NavMeshAgent _agent;
    float _chaseRange = 2f;
    float _attackRange = 1.2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 1.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_player.position);

        float distance = Vector3.Distance(_player.position, animator.transform.position);

        if (distance > _chaseRange)
        {
            animator.SetBool("isChasing", false);
        }
        else if (distance < _chaseRange)
        {
            if (Player.PlayerInstance.playerStats.health <= 0)
            {
                animator.SetBool("isChasing", false);
            }
        }

        if (distance < _attackRange)
        {
            if (Player.PlayerInstance.playerStats.health <= 0)
            {
                animator.SetBool("isAttacking", false);
            }
            else
            {
                animator.SetBool("isAttacking", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(animator.transform.position);
    }
}

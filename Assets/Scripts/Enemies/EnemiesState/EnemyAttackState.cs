using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyAttackState : StateMachineBehaviour
{
    Transform _player;
    //Transform _ally;

    float _attackRange = 1.2f;

    NavMeshAgent _agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //_ally = GameObject.FindGameObjectWithTag("Ally").transform;
        _agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(_player);


        float distanceToStopAttackPlayer = Vector3.Distance(_player.position, animator.transform.position);
        //float distanceToStopAttackAlly = Vector3.Distance(_ally.position, animator.transform.position);

        if (distanceToStopAttackPlayer < _attackRange)
        {
            animator.SetBool("isAttacking", false);
        }

        /*if (distanceToStopAttackAlly < _attackRange)
        {
            animator.SetBool("isAttacking", false);
        }*//*else
        {
            animator.transform.LookAt(_ally);
        }*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(animator.transform.position);
    }
}

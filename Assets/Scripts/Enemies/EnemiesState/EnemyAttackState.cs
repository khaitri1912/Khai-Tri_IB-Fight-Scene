using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class EnemyAttackState : StateMachineBehaviour
{
    Transform _player;
    
    Transform _ally;
    float _allyHealth;

    float _attackRange = 1.2f;

    NavMeshAgent _agent;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform _closestTarget = GetClosestTarget(_player, _ally, animator.transform);
        
        animator.transform.LookAt(_closestTarget);

        float distanceToStopAttackTarget = Vector3.Distance(_closestTarget.position, animator.transform.position);
        
        if (distanceToStopAttackTarget < _attackRange)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(animator.transform.position);
    }

    Transform GetClosestTarget(Transform _player, Transform _ally, Transform animatorTransform)
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
            return _ally;
        }
    }
}

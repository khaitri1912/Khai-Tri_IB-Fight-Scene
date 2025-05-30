using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : StateMachineBehaviour
{
    Transform _player;
    Transform _ally;
    float _allyHealth;
    NavMeshAgent _agent;
    float _chaseRange = 2f;
    float _attackRange = 1.2f;

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
            Debug.Log("enemy chase : Can't find ally");
        }

        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 1.5f;

        if (_ally == null)
        {
            return;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform _closestTarget = GetClosestTarget(_player, _ally, animator.transform);

        Debug.Log("Enemy Chase: "+_closestTarget.name);

        _agent.SetDestination(_closestTarget.position);

        float distance = Vector3.Distance(_closestTarget.position, animator.transform.position);
        
        if (distance > _chaseRange)
        {
            animator.SetBool("isChasing", false);
        }
        else if (distance < _chaseRange)
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
            }
            else
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

        if (distance < _attackRange)
        {
            if (_allyHealth != -10)
            {
                if (Player.PlayerInstance.playerStats.health <= 0
                || Ally.allyInstance.allyStats.health <= 0)
                {
                    animator.SetBool("isAttacking", false);
                }
                else
                {
                    animator.SetBool("isAttacking", true);
                }
            }
            else
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
            Debug.Log(_ally.name);
            return _ally;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : StateMachineBehaviour
{
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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer += Time.deltaTime;
        if (_Timer > 2)
        {
            animator.SetBool("isPatrolling", true);
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
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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

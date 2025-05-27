using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : StateMachineBehaviour
{
    private float _Timer;

    Transform _player;
    float _chaseRange = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer = 0;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer += Time.deltaTime;
        if (_Timer > 2)
        {
            animator.SetBool("isPatrolling", true);
        }

        float distanceToChase = Vector3.Distance(_player.position, animator.transform.position);

        if (distanceToChase < _chaseRange)
        {
            if (Player.PlayerInstance.playerStats.health <= 0)
            {
                animator.SetBool("isChasing", false);
            }else
            {
                animator.SetBool("isChasing", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

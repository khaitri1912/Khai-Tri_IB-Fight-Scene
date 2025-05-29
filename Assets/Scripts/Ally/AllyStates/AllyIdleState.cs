using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyIdleState : StateMachineBehaviour
{
    private float _timer;

    Transform _enemy;
    float _chaseRange = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0;
        _enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        if(_enemy == null)
        {
            Debug.Log("Ally khong thay enemy");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;

        if (_timer > 2)
        {
            animator.SetBool("isAllyPatrolling", true);
        }

        float distanceToChase = Vector3.Distance(_enemy.position, animator.transform.position);

        if (distanceToChase < _chaseRange)
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
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}

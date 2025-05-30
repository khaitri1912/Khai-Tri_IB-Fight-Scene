using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyChaseState : StateMachineBehaviour
{
    //Transform _enemy;

    List<Transform> enemies = new List<Transform>();

    NavMeshAgent _agent;

    float _chaseRange = 2.5f;
    float _attackRange = 1f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 1.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform closestEnemy = Enemies(animator);

        if (closestEnemy == null)
        {
            animator.SetBool("isAllyVictory", true);
            return;
        }

        _agent.SetDestination(closestEnemy.position);

        float distance = Vector3.Distance(closestEnemy.position, animator.transform.position);

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
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(animator.transform.position);
    }

    Transform Enemies(Animator animator)
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        
        if (enemy == null)
        {
            return null;
        }

        foreach (Transform t in enemy.transform)
        {
            Transform enemyTransform = t;
            enemies.Add(enemyTransform);
        }

        if (enemies.Count == 0 || enemies == null)
        {
            return null;
        }

        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Transform e in enemies)
        {
            if (e == null)
            {
                continue;
            }

            float distance = Vector3.Distance(e.position, animator.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestEnemy = e;
            }
        }
        return closestEnemy;
    }
}

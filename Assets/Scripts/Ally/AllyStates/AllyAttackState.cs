using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAttackState : StateMachineBehaviour
{

    //Transform _enemy;
    List<Transform> enemies = new List<Transform>();

    float _attackRange = 1f;

    NavMeshAgent _agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent = animator.GetComponent<NavMeshAgent>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform closestEnemy = Enemies(animator);

        if (closestEnemy == null)
        {
            return;
        }

        animator.transform.LookAt(closestEnemy);

        

        float distanceToStopAttack = Vector3.Distance(closestEnemy.position, animator.transform.position);

        if (distanceToStopAttack < _attackRange)
        {
            animator.SetBool("isAllyAttacking", false);
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

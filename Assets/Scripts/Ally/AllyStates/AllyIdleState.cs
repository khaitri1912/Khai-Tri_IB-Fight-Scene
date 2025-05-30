using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyIdleState : StateMachineBehaviour
{
    private float _timer;

    //Transform _enemy;
    List<Transform> enemies = new List<Transform>();
    float _chaseRange = 2.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform closestEnemy = Enemies(animator);

        if (closestEnemy == null)
        {
            return;
        }

        _timer += Time.deltaTime;

        if (_timer > 2)
        {
            animator.SetBool("isAllyPatrolling", true);
        }

        float distanceToChase = Vector3.Distance(closestEnemy.position, animator.transform.position);

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

    Transform Enemies(Animator animator)
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

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

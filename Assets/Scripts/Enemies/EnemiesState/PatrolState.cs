using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    List<Transform> enemyPoints = new List<Transform>();
    NavMeshAgent agent;

    private float _Timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer = 0;

        agent = animator.GetComponent<NavMeshAgent>();
        GameObject go = GameObject.FindGameObjectWithTag("EnemyPoints");

        foreach (Transform t in go.transform)
        {
            enemyPoints.Add(t);
        }

        agent.SetDestination(enemyPoints[Random.RandomRange(0, enemyPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer += Time.deltaTime;
        if (_Timer > 2)
        {
            animator.SetBool("isPatrolling", false);
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(enemyPoints[Random.RandomRange(0, enemyPoints.Count)].position);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : StateMachineBehaviour
{
    List<Transform> enemyPoints = new List<Transform>();
    NavMeshAgent _agent;

    private float _Timer;

    Transform _player;
    float _chaseRange = 2f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer = 0;

        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _agent = animator.GetComponent<NavMeshAgent>();
        _agent.speed = 0.5f;

        GameObject go = GameObject.FindGameObjectWithTag("EnemyPoints");

        foreach (Transform t in go.transform)
        {
            enemyPoints.Add(t);
        }

        _agent.SetDestination(enemyPoints[Random.RandomRange(0, enemyPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _Timer += Time.deltaTime;
        if (_Timer > 8)
        {
            animator.SetBool("isPatrolling", false);
        }

        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            _agent.SetDestination(enemyPoints[Random.RandomRange(0, enemyPoints.Count)].position);
        }

        float distanceToChase = Vector3.Distance(_player.position, animator.transform.position);

        if (distanceToChase < _chaseRange)
        {
            animator.SetBool("isChasing", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _agent.SetDestination(_agent.transform.position);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : BaseStateMachine
{
    public override void EnterState(Player playerState)
    {
        playerState.playerAnimator.SetBool("isAttacking", true);
    }

    public override void UpdateState(Player playerState)
    {
        float distance = Vector3.Distance(playerState.transform.position, playerState.enemy.transform.position);
        if (distance > playerState.distanceToEnemies)
        {
            if (playerState.inputDirection.magnitude < 0.1f)
            {
                ExitState(playerState, playerState.playerIdleState);
            }
            else
            {
                ExitState(playerState, playerState.playerWalkState);
            }
        } else if(Enemy.enemyInstance.enemyStats.health <= 2)
        {
            ExitState(playerState, playerState.playerIdleState);
        }
    }

    public void ExitState(Player playerState, BaseStateMachine state)
    {
        playerState.playerAnimator.SetBool("isAttacking", false);
        playerState.SwitchState(state);
    }
}

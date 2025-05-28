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
        if (playerState.enemy != null)
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
            }
            else if (Enemy.enemyInstance.enemyStats.health <= 1)
            {
                ExitState(playerState, playerState.playerVictoryState);
            }
        }

        if (playerState.enemy == null)
        {
            ExitState(playerState, playerState.playerVictoryState);
        }
        
        /*if (distance > playerState.distanceToEnemies)
        {
            if (playerState.inputDirection.magnitude < 0.1f)
            {
                ExitState(playerState, playerState.playerIdleState);
            }
            else
            {
                ExitState(playerState, playerState.playerWalkState);
            }
        } else if(Enemy.enemyInstance.enemyStats.health <= 1)
        {
            ExitState(playerState, playerState.playerVictoryState);
        }*/
    }

    public void ExitState(Player playerState, BaseStateMachine state)
    {
        playerState.playerAnimator.SetBool("isAttacking", false);
        playerState.SwitchState(state);
    }
}

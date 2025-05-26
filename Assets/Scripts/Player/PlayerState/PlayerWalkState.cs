using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : BaseStateMachine
{
    public override void EnterState(Player playerState)
    {
        playerState.playerAnimator.SetBool("isWalking", true);
    }

    public override void UpdateState(Player playerState)
    {
        if (playerState.inputDirection.magnitude < 0.1f)
        {
            ExitState(playerState, playerState.playerIdleState);
        }
    }

    public void ExitState(Player playerState, BaseStateMachine state)
    {
        playerState.playerAnimator.SetBool("isWalking", false);
        playerState.SwitchState(state);
    }
}

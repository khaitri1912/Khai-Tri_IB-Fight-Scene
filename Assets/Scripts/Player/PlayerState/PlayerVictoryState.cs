using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVictoryState : BaseStateMachine
{
    public override void EnterState(Player playerState)
    {
        playerState.playerAnimator.SetBool("isVictory", true);
    }

    public override void UpdateState(Player playerState)
    {
        //ExitState(playerState, playerState.playerIdleState);
    }

    public void ExitState(Player playerState, BaseStateMachine state)
    {
        playerState.playerAnimator.SetBool("isVictory", false);
        playerState.SwitchState(state);
    }
}

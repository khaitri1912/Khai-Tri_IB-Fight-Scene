using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : BaseStateMachine
{
    public override void EnterState(Player playerState)
    {
        
    }

    public override void UpdateState(Player playerState)
    {
        if (playerState.inputDirection.magnitude > 0.1f)
        {
            playerState.SwitchState(playerState.playerWalkState);
        }
    }
}

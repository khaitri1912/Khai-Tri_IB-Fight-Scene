using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine
{
    public abstract void EnterState(Player playerState);
    public abstract void UpdateState(Player playerState);
}

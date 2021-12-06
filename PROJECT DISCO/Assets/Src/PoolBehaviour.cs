using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBehaviour : RoomBehaviour
{
    public override void logic()
    {
        if (!roomEnabled)
            return;
    }

    public override void enable()
    {
        base.enable();
    }

    public override void disable()
    {
        base.disable();
    }
}

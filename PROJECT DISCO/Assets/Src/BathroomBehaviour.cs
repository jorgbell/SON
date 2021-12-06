using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomBehaviour : RoomBehaviour
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

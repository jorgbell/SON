using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBehaviour : RoomBehaviour
{
    private float nightValue = 0f;
    private float nextTime = 0.0f;
    public float nightValueChangeTime = 0.1f; //seconds
    public float changePerTime = 0.01f; //each nightValueChangeTime it changes this value
    public FMODUnity.StudioEventEmitter ost;

    private void Update()
    {
        if (!roomEnabled)
            return;
        float time = Time.time;
        if (time >= nextTime + nightValueChangeTime)
        {
            nextTime = time + nightValueChangeTime;

            if (nightValue >= 1f)
            {
                nightValue = 1;
                changePerTime *= -1; //va hacia atrás
            }
            else if (nightValue <= 0f)
            {
                nightValue = 0;
                changePerTime *= -1;
            }

            nightValue += changePerTime;
            ost.SetParameter("NightValue", nightValue);
        }
    }

    public override void enable()
    {
        base.enable();
        nightValue = 0f;
    }
    public override void disable()
    {
        base.disable();
        nightValue = 0f;
    }
}

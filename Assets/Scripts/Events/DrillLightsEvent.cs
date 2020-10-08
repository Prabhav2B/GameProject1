using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillLightsEvent : TriggeredEvent
{
    public override void OnEvent()
    {
        //turn lights on
        base.OnEvent();
    }

    public override void OnEventExit()
    {
        //turn lights off
        base.OnEventExit();
    }
}

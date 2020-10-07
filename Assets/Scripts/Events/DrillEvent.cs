using System.Collections.Generic;
using UnityEngine;

public class DrillEvent : TriggeredEvent
{
    public GameObject RocksParent;

    List<Rigidbody2D> rocks;

    private void Start()
    {
        rocks = new List<Rigidbody2D>(RocksParent.GetComponentsInChildren<Rigidbody2D>());
    }

    public override void OnEvent()
    {
        TriggerRocks();
    }

    void TriggerRocks()
    {
        foreach (var rock in rocks)
        {
            rock.isKinematic = false;
        }

    }

    
}

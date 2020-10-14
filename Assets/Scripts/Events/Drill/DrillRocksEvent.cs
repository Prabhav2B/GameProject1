using System.Collections.Generic;
using UnityEngine;

public class DrillRocksEvent : TriggeredEvent
{
    public GameObject RocksParent;
    [SerializeField] AudioSource au;

    List<Rigidbody2D> rocks;

    private void Start()
    {
        rocks = new List<Rigidbody2D>(RocksParent.GetComponentsInChildren<Rigidbody2D>());
    }

    public override void OnEvent()
    {
        au.Play();
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

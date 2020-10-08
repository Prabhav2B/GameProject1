using UnityEngine;

public class TriggeredEvent : MonoBehaviour
{
    [SerializeField]private bool isOneOff = false;
    public virtual void OnEvent() { return; }

    public virtual void OnEventExit() { return; }
}

using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] List<TriggeredEvent> Events;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            foreach (var ev in Events)
            {
                ev.OnEvent();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + new Vector3(this.GetComponent<CircleCollider2D>().offset.x, 
            this.GetComponent<CircleCollider2D>().offset.y, 0),
            this.GetComponent<CircleCollider2D>().radius);
    }
}

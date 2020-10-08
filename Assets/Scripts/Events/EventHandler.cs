using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] List<TriggeredEvent> PassiveEvents;
    [SerializeField] List<TriggeredEvent> ActiveEvents;

    bool hasActivated = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasActivated = true;
            Invoke("Deactivate", .1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {

            foreach (var ev in PassiveEvents)
            {
                ev.OnEvent();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (hasActivated)
            {
                hasActivated = false;
                foreach (var ev in ActiveEvents)
                {
                    ev.OnEvent();
                }
            }

            //foreach (var ev in PassiveEvents)
            //{
            //    ev.OnEvent();
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {

            foreach (var ev in ActiveEvents)
            {
                ev.OnEventExit();
            }

            foreach (var ev in PassiveEvents)
            {
                ev.OnEventExit();
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

    private void Deactivate()
    {
        hasActivated = false;
    }
}

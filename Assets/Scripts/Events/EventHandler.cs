using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] DeathController dc;
    [SerializeField] GameObject idleDrill;
    [SerializeField] GameObject animatedDrill;
    [SerializeField] GameObject sparks;

    [SerializeField] List<TriggeredEvent> PassiveEvents;
    [SerializeField] List<TriggeredEvent> ActiveEvents;

    float drillTimer;
    float drillTime = 2f;

    bool hastriggered = false;

    private void Start()
    {
        idleDrill.SetActive(true);
        animatedDrill.SetActive(false);
        sparks.SetActive(false);

        dc.OnDeath += ResetDrill;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.layer == 8)
        {
            drillTimer = 0f;
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
            if (drillTimer >= drillTime && !hastriggered)
            {
                hastriggered = true;
                drillTimer = 0f;

                idleDrill.SetActive(false);
                animatedDrill.SetActive(true);
                sparks.SetActive(true);

                foreach (var ev in ActiveEvents)
                {
                    ev.OnEvent();
                }
            }
            else
            {
                drillTimer += Time.deltaTime;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        idleDrill.SetActive(true);
        animatedDrill.SetActive(false);
        sparks.SetActive(false);
        if (collision.gameObject.layer == 8)
        {
            drillTimer = 0f;
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

    void ResetDrill()
    {
        hastriggered = false;

        idleDrill.SetActive(true);
        animatedDrill.SetActive(false);
        sparks.SetActive(false);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + new Vector3(this.GetComponent<CircleCollider2D>().offset.x, 
            this.GetComponent<CircleCollider2D>().offset.y, 0),
            this.GetComponent<CircleCollider2D>().radius);
    }
}

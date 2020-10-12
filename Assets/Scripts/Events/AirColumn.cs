using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirColumn : MonoBehaviour
{
    [SerializeField] private float force = 10f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>().ExternalForce( (Vector2)this.transform.up * force);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<PlayerController>().ResetExternalForce();
    }
}

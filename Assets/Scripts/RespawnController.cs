using UnityEngine;

[RequireComponent(typeof(DeathController))]
public class RespawnController : MonoBehaviour
{
    [SerializeField] int checkpointLayer = 10;

    DeathController dc;

    private void Start()
    {
        dc = GetComponent<DeathController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == checkpointLayer)
        {
            dc.RespawnTransform = collision.transform.parent;
        }
    }

}

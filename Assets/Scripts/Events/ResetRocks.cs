
using UnityEngine;

public class ResetRocks : MonoBehaviour
{
    private Vector2 myPos;
    private Rigidbody2D rb;

    [SerializeField] private DeathController dc;

    void Start()
    {
        
        myPos = this.transform.position;
        rb = this.GetComponent<Rigidbody2D>();

        dc.OnDeath += ResetSelf;
    }

    void ResetSelf()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.isKinematic = true;
        rb.MovePosition(myPos);
    }
}

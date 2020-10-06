using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private Transform respawnTransform;
    [SerializeField] int killPlayerLayer = 9;

    public Transform RespawnTransform { private get { return respawnTransform; } set { respawnTransform = value; } }
    
    public delegate void deathDel();
    public event deathDel OnDeath;

    void FixedUpdate()
    {
        RaycastHit2D hit;
        if (Physics2D.CircleCast(this.transform.position, .5f, Vector2.down, .1f))
        {
            hit = Physics2D.CircleCast(this.transform.position, .5f, Vector2.down, .1f);

            if (hit.collider.gameObject.layer == 11)
            {
                if (this.GetComponent<Rigidbody2D>().velocity.y < -1f)
                {
                    this.transform.position = RespawnTransform.position;
                    OnDeath();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == killPlayerLayer)
        {
            this.transform.position = RespawnTransform.position;
            OnDeath();
        }

        
    }

}

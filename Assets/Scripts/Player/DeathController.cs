using System;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private Transform respawnTransform;
    [SerializeField] int killPlayerLayer = 9;
    [SerializeField] GameObject player;
    [SerializeField] GameObject deathParticles;
    [SerializeField] Transform particlesParent;


    public Transform RespawnTransform { private get { return respawnTransform; } set { respawnTransform = value; } }
    
    public delegate void deathDel();
    public event deathDel OnDeath;

    private void Start()
    {
        OnDeath += DeathParticles;
    }

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
            OnDeath();
            this.GetComponentInChildren<SpriteRenderer>().enabled = false;
            Invoke("ResetPlayerPosition", 2f);
        }
    }

    void ResetPlayerPosition()
    {
        this.GetComponentInChildren<SpriteRenderer>().enabled = true; 

        this.transform.position = RespawnTransform.position;

    }

    private void DeathParticles()
    {
        GameObject go = Instantiate(deathParticles, this.transform.position, Quaternion.identity);
        go.transform.parent = particlesParent;
        Destroy(go, 1f);
    }


}

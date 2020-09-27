using System.Collections;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [SerializeField] private Transform respawnTransform;
    [SerializeField] LayerMask killPlayer = 1 >> 9;

    public Transform RespawnTransform { private get { return respawnTransform; } set { respawnTransform = value; } }
    
    public delegate void deathDel();
    public event deathDel OnDeath;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            this.transform.position = respawnTransform.position;
            OnDeath();
        }
    }

}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimManager : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();

        if (anim == null)
        {
            Debug.LogError("Missing Animator!");
        }
    }

    private void Update()
    {
        anim.SetFloat("Horizontal", rb.velocity.x);
        anim.SetFloat("Vertical", rb.velocity.y);

    }
}

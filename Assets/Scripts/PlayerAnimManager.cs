using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimManager : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Dir dir;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponentInChildren<Animator>();
        dir = Dir.idle;

        if (anim == null)
        {
            Debug.LogError("Missing Animator!");
        }
        
    }

    private void Update()
    {
        //anim.SetFloat("Horizontal", rb.velocity.x);
        //anim.SetFloat("Vertical", rb.velocity.y);

        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(rb.velocity.y))
        {
            if (rb.velocity.x > 0 && dir != Dir.right)
            {
                dir = Dir.right;
                anim.SetTrigger("right");
            }
            else if (rb.velocity.x < 0 && dir != Dir.left)
            {
                dir = Dir.left;
                anim.SetTrigger("left");
            }
        }
        else
        {
            if (rb.velocity.y > 0 && dir != Dir.up)
            {
                dir = Dir.up;
                anim.SetTrigger("up");
            }
            else if (rb.velocity.y < 0 && dir != Dir.down)
            {
                dir = Dir.down;
                anim.SetTrigger("down");
            }
        }

    }

    public enum Dir {right, left, up, down, idle}
}

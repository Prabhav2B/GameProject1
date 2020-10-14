using UnityEngine;

public class AirColumn : MonoBehaviour
{
    [SerializeField] private float force = 10f;
    AudioSource au;
    private bool flipped = false;

    private ParticleSystem steamFx;

    private void Start()
    {
        steamFx = this.GetComponentInChildren<ParticleSystem>();

        au = this.GetComponent<AudioSource>();
        au.loop = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flipped)
        {
            collision.gameObject.GetComponent<PlayerController>().ExternalForce((Vector2)this.transform.up * force);
        }
        else
        {
            collision.gameObject.GetComponent<PlayerController>().ResetExternalForce();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            collision.gameObject.GetComponent<PlayerController>().ResetExternalForce();
    }

    public void Flip()
    {
        flipped = !flipped;

        if (flipped)
        {
            au.Stop();
            steamFx.Stop();
        }
        else
        {
            au.Play();
            steamFx.Play();
        }

    }
}

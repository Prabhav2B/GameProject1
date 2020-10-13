using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(AudioSource))]

public class ParticleSound : MonoBehaviour
{

    AudioSource au;
    ParticleSystem ps;

    [SerializeField] private AudioClip[] effectClip;

    bool clipPlayed;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        au = GetComponent<AudioSource>();

        au.clip = effectClip[Random.Range(0, effectClip.Length)];
        clipPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.subEmitters.GetSubEmitterSystem(0).isPlaying && clipPlayed == false)
        {
            au.clip = effectClip[Random.Range(0, effectClip.Length)];
            au.Play();
            clipPlayed = true;
        }
        else
        {
            clipPlayed = false;
        }
    }
}

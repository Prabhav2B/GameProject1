using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMoosic : MonoBehaviour
{
    [SerializeField]AudioSource chords;
    [SerializeField]AudioSource melody;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        chords.loop = true;
        melody.loop = true;

        chords.Play();
        melody.Play();
    }
}

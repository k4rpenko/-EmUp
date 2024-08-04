using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soungs : MonoBehaviour
{
    public AudioClip[] soungs;
    private AudioSource audioSRC => GetComponent<AudioSource>();

    public void PlaySoungs(AudioClip clip, float volume = 1f, bool destroed = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        audioSRC.pitch = Random.Range(p1, p2);
        audioSRC.PlayOneShot(clip, volume);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip deathSound;
    public static AudioClip introSound;
    public static AudioClip pelletSound;
    public static AudioClip victorySound;
    public static AudioClip eatGhostSound;
    public static AudioClip ghostPanicSound;
    static AudioSource audioSource;
    string currentlyPlaying;

    void Start()
    {
        introSound = Resources.Load<AudioClip>("Audio/Intro");
        deathSound = Resources.Load<AudioClip>("Audio/Death");
        pelletSound = Resources.Load<AudioClip>("Audio/Pellet");
        victorySound = Resources.Load<AudioClip>("Audio/Victory");
        eatGhostSound = Resources.Load<AudioClip>("Audio/EatGhost");
        ghostPanicSound = Resources.Load<AudioClip>("Audio/GhostPanic");
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        switch (clip) {
            case "intro":
                audioSource.PlayOneShot(introSound);
                break;
            case "pellet":
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(pelletSound);
                }
                break;
            case "ghostPanic":
                audioSource.PlayOneShot(ghostPanicSound);
                break;
            case "eatGhost":
                audioSource.PlayOneShot(eatGhostSound);
                break;
            case "death":
                audioSource.PlayOneShot(deathSound);
                break;
            case "victory":
                audioSource.PlayOneShot(victorySound);
                break;
        }
    }
}

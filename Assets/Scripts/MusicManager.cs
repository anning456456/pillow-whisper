using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    public static MusicManager Instance;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayAduioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}

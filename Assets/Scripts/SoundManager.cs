using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    FootStep,
    Sword,
    Hurt,
    Death,
    rope,
    cannon,
    ocean,
    shout,



}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundlist;
    private static SoundManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public static void PlaySound(SoundType sound, float volume = 1)
    {
        instance.audioSource.PlayOneShot(instance.soundlist[(int)sound], volume);
    }
}

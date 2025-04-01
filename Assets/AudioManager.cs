using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Sorce ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;


    [Header("---------- Audio Clip ----------")]
    public AudioClip MainMenu;

    public AudioClip Death;

    public AudioClip Walking;

    public AudioClip Cannon;

    public AudioClip Sword;


    private void Start()
    {
        musicSource.clip = MainMenu;
        musicSource.Play();
        musicSource.clip = Cannon;
        musicSource.loop = true;
        musicSource.Play();
    }


}
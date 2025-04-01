using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Sorce ----------")]
    [SerializeField] AudioSource musicSource; // For background music
    [SerializeField] AudioSource SFXSource1;  // For sound effects
    [SerializeField] AudioSource SFXSource2;  // Another for overlapping SFX

    [Header("---------- Audio Clip ----------")]
    public AudioClip MainMenu;
    public AudioClip Death;
    public AudioClip Walking;
    public AudioClip Cannon;
    public AudioClip Waves;

    private void Start()
    {
        // Play main menu music
        musicSource.clip = MainMenu;
        musicSource.loop = true;  // Ensure it loops
        musicSource.Play();

        // Play the waves sound effect
        SFXSource1.clip = Waves;
        SFXSource1.loop = true;  // Ensure it loops
        SFXSource1.Play();

        // Play the cannon sound effect
        SFXSource2.clip = Cannon;
        SFXSource2.loop = true;  // Ensure it loops
        SFXSource2.Play();
    }
}

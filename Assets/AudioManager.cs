using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Sorce ----------")]
    [SerializeField] AudioSource musicSource; 
    [SerializeField] AudioSource SFXSource1;  
    [SerializeField] AudioSource SFXSource2;
    [SerializeField] AudioSource SFXSource3;
    [SerializeField] AudioSource SFXSource4;
    [Header("---------- Audio Clip ----------")]
    public AudioClip MainMenu;
    public AudioClip RowBoat;
    public AudioClip Walking;
    public AudioClip Cannon;
    public AudioClip Waves;
    public AudioClip Bell;


    private void Start()
    {
        
        musicSource.clip = MainMenu;
        musicSource.loop = true; 
        musicSource.Play();

       
        SFXSource1.clip = Waves;
        SFXSource1.loop = true;  
        SFXSource1.Play();

      
        SFXSource2.clip = Cannon;
        SFXSource2.loop = true;  
        SFXSource2.Play();

        SFXSource3.clip = RowBoat;
       
        SFXSource3.Play();

        SFXSource4.clip = Bell;

        SFXSource4.Play();
    }
}
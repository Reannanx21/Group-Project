
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    [Header("---------- Audio Sorce ----------")]

    [SerializeField] AudioSource SFXSource5;
    [SerializeField] AudioSource SFXSource6;
    [SerializeField] AudioSource SFXSource7;


    [Header("---------- Audio Clip ----------")]
    public AudioClip Waves2;
    public AudioClip WoodCreaking;
    public AudioClip Birds;



    private void Start()
    {


        SFXSource5.clip = Waves2;
        SFXSource5.loop = true;
        SFXSource5.Play();


        SFXSource6.clip = WoodCreaking;
        SFXSource6.loop = true;
        SFXSource7.Play();

        SFXSource7.clip = Birds;

        SFXSource7.Play();





    }
}
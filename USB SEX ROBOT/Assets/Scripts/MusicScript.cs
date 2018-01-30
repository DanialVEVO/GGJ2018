using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // Publics
    public AudioClip audioClipIntro;
    public AudioClip audioClipPart;
    public AudioClip audioClipLoop;

    public AudioClip coffeeRadio;

    [SerializeField]
    AudioClip[] radioTransmissions;

    [SerializeField]
    Vector2 newTransmissionRange = new Vector2(10.0f,30.0f);


    [SerializeField]
    float musicVolume = 0.7f;

    [SerializeField]
    float ccRadioVolume = 0.7f;

    float timeTillNewTransmission = 0.0f;

    // Privates
    private AudioSource audioSourceIntro = null;
    private AudioSource audioSourcePart = null;
    private AudioSource audioSourceLoop = null;
    private AudioSource coffeeRadioSource = null;

    // Use this for initialization
    void Start()
    {
        musicVolume = Mathf.Clamp(musicVolume, 0.0f, 1.0f);

        StartMusic();

    }

    void StartMusic()
    {
        audioSourceIntro = Camera.main.gameObject.AddComponent<AudioSource>();
        audioSourcePart = Camera.main.gameObject.AddComponent<AudioSource>();
        audioSourceLoop = Camera.main.gameObject.AddComponent<AudioSource>();
        coffeeRadioSource = Camera.main.gameObject.AddComponent<AudioSource>();

        audioSourceIntro.clip = audioClipIntro;
        audioSourceIntro.volume = musicVolume;
        audioSourceIntro.PlayDelayed(2.0f);

        audioSourcePart.clip = audioClipPart;
        audioSourcePart.volume = musicVolume;
        audioSourcePart.PlayDelayed(9.680f);

        audioSourceLoop.clip = audioClipLoop;
        audioSourceLoop.volume = musicVolume;
        audioSourceLoop.loop = true;
        audioSourceLoop.PlayDelayed(25.040f);

        coffeeRadioSource.clip = coffeeRadio;
        coffeeRadioSource.volume = 0.0f;
        coffeeRadioSource.Play();
    }

    void RandomRadioTransmissions()
    {

        if (radioTransmissions.Length == 0)
            return; 
                
        int randTrans = Random.Range(0, radioTransmissions.Length);


        GetComponent<AudioSource>().PlayOneShot(radioTransmissions[randTrans]);
        timeTillNewTransmission = Random.Range(newTransmissionRange.x, newTransmissionRange.y) + radioTransmissions[randTrans].length;
    }

    void ChangeRadio()
    {
        if (coffeeRadioSource.volume == ccRadioVolume)
        {
            coffeeRadioSource.volume = 0.0f;

            audioSourceIntro.volume = musicVolume;
            audioSourcePart.volume = musicVolume;
            audioSourceLoop.volume = musicVolume;
        }
        else
        {
            coffeeRadioSource.volume = ccRadioVolume;

            audioSourceIntro.volume = 0.0f;
            audioSourcePart.volume = 0.0f;
            audioSourceLoop.volume = 0.0f;

        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (timeTillNewTransmission < 0.0f)
             RandomRadioTransmissions();

         timeTillNewTransmission -= Time.deltaTime;

        
         if (Input.GetKeyDown("joystick button 0"))
             ChangeRadio();
    }
}

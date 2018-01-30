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

    float timeTillNewTransmission = 0.0f;

    // Privates
    private AudioSource audioSourceIntro = null;
    private AudioSource audioSourcePart = null;
    private AudioSource audioSourceLoop = null;

    // Use this for initialization
    void Start()
    {
        StartMusic();
    }

    void StartMusic()
    {
        audioSourceIntro = Camera.main.gameObject.AddComponent<AudioSource>();
        audioSourcePart = Camera.main.gameObject.AddComponent<AudioSource>();
        audioSourceLoop = Camera.main.gameObject.AddComponent<AudioSource>();

        audioSourceIntro.clip = audioClipIntro;
        audioSourceIntro.volume = 0.7f;
        audioSourceIntro.PlayDelayed(2.0f);

        audioSourcePart.clip = audioClipPart;
        audioSourcePart.volume = 0.7f;
        audioSourcePart.PlayDelayed(9.680f);

        audioSourceLoop.clip = audioClipLoop;
        audioSourceLoop.volume = 0.7f;
        audioSourceLoop.loop = true;
        audioSourceLoop.PlayDelayed(25.040f);
    }

    void RandomRadioTransmissions()
    {
        AudioClip randTrans = radioTransmissions[1];
        GetComponent<AudioSource>().PlayOneShot(randTrans);
        timeTillNewTransmission = Random.Range(newTransmissionRange.x, newTransmissionRange.y);
    }

    void ChangeRadio()
    {
        if (GetComponent<AudioSource>().volume == 0)
        {
            GetComponent<AudioSource>().volume = 0.0f;

            audioSourceIntro.volume = 0.7f;
            audioSourcePart.volume = 0.7f;
            audioSourceLoop.volume = 0.7f;
        }
        else
        {
            GetComponent<AudioSource>().Play();
            
        }
    }

    // Update is called once per frame
    void Update ()
    {
        /* if (timeTillNewTransmission < 0.0f)
             RandomRadioTransmissions();

         timeTillNewTransmission -= Time.deltaTime;


         if (Input.GetKeyDown("joystick button 0"))
             ChangeRadio();*/
    }
}

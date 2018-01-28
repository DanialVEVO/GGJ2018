using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // Publics
    public AudioClip audioClipIntro;
    public AudioClip audioClipPart;
    public AudioClip audioClipLoop;

    // Privates
    private AudioSource audioSourceIntro = null;
    private AudioSource audioSourcePart = null;
    private AudioSource audioSourceLoop = null;

	// Use this for initialization
	void Start ()
    {
        audioSourceIntro = Camera.main.gameObject.AddComponent<AudioSource>();
        audioSourcePart = Camera.main.gameObject.AddComponent<AudioSource>();
        audioSourceLoop = Camera.main.gameObject.AddComponent<AudioSource>();

        audioSourceIntro.clip = audioClipIntro;
        audioSourceIntro.volume = 0.25f;
        audioSourceIntro.PlayDelayed(2.0f);

        audioSourcePart.clip = audioClipPart;
        audioSourcePart.volume = 0.25f;
        audioSourcePart.PlayDelayed(9.680f);

        audioSourceLoop.clip = audioClipLoop;
        audioSourceLoop.volume = 0.25f;
        audioSourceLoop.loop = true;
        audioSourceLoop.PlayDelayed(25.040f);
    }

    // Update is called once per frame
    void Update ()
    {

    }
}

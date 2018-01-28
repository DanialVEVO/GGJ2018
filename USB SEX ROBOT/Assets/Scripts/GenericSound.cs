using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSound : MonoBehaviour {
    AudioSource hitSound;
    public AudioClip[] audioClips;

    // Use this for initialization
    void Start () {
		hitSound = GetComponent<AudioSource>();
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Car")
        {
            hitSound.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            hitSound.Play();
        }      
    }
}

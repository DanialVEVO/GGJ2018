using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CollisionDetection : MonoBehaviour {
    public int value = 100;
    //private GameObject damageManager;
    bool hasBroken;
    bool resetMass;
    float time;
    AudioSource breakSound;
    public AudioClip[] audioClips;
    Rigidbody[] rigidBodies;

    void Awake()
    {
        // damageManager = GameObject.Find("DamageManager");
        breakSound = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
       hasBroken = false;
       resetMass = false;
       time = 0;
       rigidBodies = this.GetComponentsInChildren<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (hasBroken)
        {
            if (!resetMass && time > 0.5f)
            {
                foreach (Rigidbody R in rigidBodies)
                {
                    R.mass = 0.01f;
                }
            }
            time += Time.deltaTime;
        }
    }
		
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Car" &! hasBroken)
        {
            hasBroken = true;
            breakSound.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            breakSound.Play();
            foreach (Rigidbody R in rigidBodies)
            {
                R.isKinematic = false;
            }
            GetComponent<Collider>().enabled = false;
            
            if (other.GetComponentInChildren<PointSystem>() != null)
            {
                other.GetComponentInChildren<PointSystem>().BudgetChange(-value);
            } 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ParticleSystem particleSystem = this.gameObject.GetComponent<ParticleSystem>();

        if (!particleSystem.IsAlive())
            Destroy(this.gameObject);
	}
}

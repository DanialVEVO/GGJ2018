using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionDetection : MonoBehaviour {
    bool hasBroken;
    bool resetMass;
    int timeToDestruction;
    float time;
    Rigidbody[] rigidBodies;

    // Use this for initialization
    void Start () {
       hasBroken = false;
        resetMass = false;
       timeToDestruction = 10;
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
            if (time > timeToDestruction)
            {
                Destroy(this.gameObject);
            }
        }
    }
		
	void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" &! hasBroken)
        {
            hasBroken = true;
            foreach (Rigidbody R in rigidBodies)
            {
                R.isKinematic = false;
            }
            GetComponent<Collider>().enabled = false;
        }
    }
}

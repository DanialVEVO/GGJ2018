using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationInfo : MonoBehaviour
{
    public string currentLocation = "";

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Sets the location
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "LocationTrigger")
            currentLocation = other.gameObject.name;
    }
}

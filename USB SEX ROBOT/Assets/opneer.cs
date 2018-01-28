using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opneer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(0, Mathf.PingPong(Time.time * 30.0f, 3) * 0.03f, 0);


    }
}

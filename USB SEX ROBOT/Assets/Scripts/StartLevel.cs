using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartLevel : MonoBehaviour {

    public AudioClip geluitje;
    private AudioSource saus;

    // Use this for initialization
    void Start () {
        //SceneManager.sceneLoaded
        saus = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {

        if (Input.anyKeyDown) { 
        saus.PlayOneShot(geluitje);
      //  Debug.Log("Bevroren kontjes");
      //  Debug.Log(Input.anyKeyDown);
        SceneManager.LoadScene("FinalLevel", LoadSceneMode.Single);
    }

    }
}

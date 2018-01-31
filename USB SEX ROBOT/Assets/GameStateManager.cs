using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    [SerializeField]
    Camera playerCam;

    [SerializeField]
    Camera endCam;


	// Use this for initialization
	void Start () {
		
	}

    // ENDSEQUENCE
    public void StartEndSequence()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);

        //Switch to Camera parented above player that zooms out slowly
        //playerCam.enabled = false;
        //endCam.enabled = true;
        // Do fucking model swap to Trumpede car (taken from Mounim's main menu)

    }
}

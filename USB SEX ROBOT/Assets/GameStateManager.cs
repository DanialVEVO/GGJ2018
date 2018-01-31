using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

    [SerializeField]
    GameObject playerCam;

    [SerializeField]
    GameObject endCam;

    [SerializeField]
    GameObject endScreenCar;

    [HideInInspector]
    public bool sequenceStarted;

    // Use this for initialization
    void Start ()
    {
        sequenceStarted = false;	
	}

    /* /// DEBUG FOR END SEQUENCE TESTING PURPOSES
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartEndSequence();

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerCam.SetActive(true);
            endCam.SetActive(false);
        }
    }
    */


    // ENDSEQUENCE
    public void StartEndSequence()
    {
        if (sequenceStarted == false)
        {
        // Switch to Camera parented above player that zooms out slowly
        playerCam.SetActive(false);
        endCam.SetActive(true);

        // Do  model swap to Trumpede car since player model isn't actual model
        GameObject car = Instantiate(endScreenCar);
        car.transform.parent = gameObject.transform;
        car.transform.localPosition = new Vector3(0,0,0);

        Quaternion rotationToSet = gameObject.transform.localRotation;
        car.transform.localRotation = Quaternion.identity;

        // Play zoomout animation
        Animator anim = endCam.GetComponent<Animator>();
        anim.SetBool("ZoomOut", true);

        StartCoroutine(DelayUntilRestart());

            //bool startedZoom? = Animator.StringToHash("ZoomOut");
            //anim.StringToHash(startedZoom);
        }
    }

    IEnumerator DelayUntilRestart()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}

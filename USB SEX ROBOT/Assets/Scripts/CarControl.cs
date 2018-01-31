using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour {

    [SerializeField]
    float maxSpeed = 20.0f;

    [SerializeField]
    float maxAcceleration = 2.0f;

    [SerializeField]
    float maxBreakSpeed = 2.0f;

    [SerializeField]
    float turnSpeed = 2.0f;

    [SerializeField]
    float turnAdjustSpeed = 2.0f;

    [SerializeField]
    Vector2 areaFOV = new Vector2(50, 70);

    [SerializeField]
    GameObject SteeringWHeel;

    [SerializeField]
    float steeringWheelSpeed = 20.0f;

    [SerializeField]
    float SteeringWheelClip = 90.0f;

    [SerializeField]
    float sideLookClip = 30.0f;

    [SerializeField]
    float maxLookSpeed = 150.0f;

    [SerializeField][Tooltip ("accel, decel, idle, 3x tyres")]
    AudioClip[] allCarSounds;

    Quaternion toRotation = new Quaternion();
    //Vector3 adjustDir = Vector3.zero;

    float TimeNotMovingForward = 0.0f;

    float SteeringWheelRotation = 0.0f;

    bool driftSoundReady = true;

    Vector3 startLookAngle;

    float oldYangle = 0.0f;


    // Use this for initialization
    void Start () {

        toRotation = GetComponent<Rigidbody>().rotation;

        GetComponent<AudioSource>().clip = allCarSounds[2];
        GetComponent<AudioSource>().Play();

        startLookAngle = Camera.main.transform.localEulerAngles;

    }
	
	// Update is called once per frame
	void FixedUpdate () {


        toRotation = GetComponent<Rigidbody>().rotation;

        Vector3 localVelocity = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity);

        //check how long the car not moving forward.
        if (localVelocity.z < 0.1f)
        {
            TimeNotMovingForward += Time.fixedDeltaTime;
            
        }
        else if (TimeNotMovingForward != 0.0f)
            TimeNotMovingForward = 0.0f;



        //Field of view adjustment for extra sppeeeed.        
        float percentageFOV = 0; 

        if (GetComponent<Rigidbody>().velocity.magnitude > maxSpeed / 4)
            percentageFOV = (GetComponent<Rigidbody>().velocity.magnitude - maxSpeed / 4) / (maxSpeed * 0.75f);

        GetComponentInChildren<Camera>().fieldOfView = areaFOV.x + (areaFOV.y - areaFOV.x) * percentageFOV;

        //turning
        if (Input.GetAxis("LeftX") !=0 && GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
        {

            Vector3 rotationQuantity = Vector3.zero;

            if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed / 4)
            {
                float turnLimit = GetComponent<Rigidbody>().velocity.magnitude / (maxSpeed / 4);
                rotationQuantity = new Vector3(0.0f, Input.GetAxis("LeftX"), 0.0f) * turnLimit * turnSpeed * Time.fixedDeltaTime;
            }
            else
                rotationQuantity = new Vector3(0.0f, Input.GetAxis("LeftX"), 0.0f) * turnSpeed * Time.fixedDeltaTime;

            
            if (localVelocity.z < 0)
                rotationQuantity = -rotationQuantity;

            Quaternion deltaRotation = Quaternion.Euler(rotationQuantity);
            toRotation = GetComponent<Rigidbody>().rotation * deltaRotation;

            if (localVelocity.z < 0)
                rotationQuantity = -rotationQuantity;


            //steering wheel turn
            if (rotationQuantity.y > 0 && SteeringWheelRotation > -SteeringWheelClip)
            {
                SteeringWHeel.transform.Rotate(0, 0, -rotationQuantity.y * steeringWheelSpeed * Time.deltaTime);
                SteeringWheelRotation += -rotationQuantity.y * steeringWheelSpeed * Time.deltaTime;
            }
            else if (rotationQuantity.y < 0 && SteeringWheelRotation < SteeringWheelClip)
            {
                SteeringWHeel.transform.Rotate(0, 0, -rotationQuantity.y * steeringWheelSpeed * Time.deltaTime);
                SteeringWheelRotation += -rotationQuantity.y * steeringWheelSpeed * Time.deltaTime;
            }

            
        }
        else
        {
            // when not steering, set steeringwheel back to neutral position.

            if (SteeringWheelRotation > 1.0f)
            {
                SteeringWHeel.transform.Rotate(0, 0, -steeringWheelSpeed * Time.deltaTime);
                SteeringWheelRotation +=  -steeringWheelSpeed * Time.deltaTime;
            }

            if (SteeringWheelRotation < 1.0f)
            {
                SteeringWHeel.transform.Rotate(0, 0, steeringWheelSpeed * Time.deltaTime);
                SteeringWheelRotation += steeringWheelSpeed * Time.deltaTime;
            }
        }

    
        //dont tip the car

        if (toRotation.eulerAngles.x > 0.01f || toRotation.eulerAngles.z > 0.01f)
            toRotation = Quaternion.Euler(0, toRotation.eulerAngles.y, 0);

        if (toRotation != GetComponent<Rigidbody>().rotation)
            GetComponent<Rigidbody>().MoveRotation(toRotation);



        
        //adjust force to rotation
        if (GetComponent<Rigidbody>().velocity.magnitude > 0.1f && Vector3.Angle(transform.forward, GetComponent<Rigidbody>().velocity) > 0.1f)
        {
            Vector3 adjustDir = -Vector3.ProjectOnPlane(GetComponent<Rigidbody>().velocity, transform.forward);

            GetComponent<Rigidbody>().AddForce(adjustDir * turnAdjustSpeed * Time.fixedDeltaTime);

        }

        //acceleration
        if ((Input.GetAxis("RightBump") > 0.03f && Input.GetAxis("LeftBump") > 0.03f) || (Input.GetAxis("RightBump") < 0.03f && Input.GetAxis("LeftBump") < 0.03f))
        {
            if (GetComponent<AudioSource>().clip != allCarSounds[1] && GetComponent<AudioSource>().clip != allCarSounds[2])
            {
                
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = allCarSounds[1];
                GetComponent<AudioSource>().PlayOneShot(allCarSounds[1]);
            }
            else if (GetComponent<AudioSource>().clip == allCarSounds[1] && !GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = allCarSounds[2];
                GetComponent<AudioSource>().Play();
            }

            if (driftSoundReady == true && GetComponent<Rigidbody>().velocity.magnitude > maxBreakSpeed / 2 && Input.GetAxis("RightBump") > 0.03f && Input.GetAxis("LeftBump") > 0.03f)
            {
                GetComponent<AudioSource>().PlayOneShot(allCarSounds[4]);
                driftSoundReady = false;
            }
        }
        else if (Input.GetAxis("RightBump") > 0 && GetComponent<Rigidbody>().velocity.magnitude <= maxSpeed)
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * maxAcceleration * Input.GetAxis("RightBump") * Time.fixedDeltaTime);

            if (GetComponent<AudioSource>().clip != allCarSounds[0])
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().clip = allCarSounds[0];
                GetComponent<AudioSource>().Play();
            }
        }
        else if (Input.GetAxis("LeftBump") > 0)
        {


            //brake when having forwardspeed, speed at all (to keep sliding)
            if (localVelocity.z > 0.1f)
            {
                //start drift sounds, make sure it doesnt start again.
                if (driftSoundReady == true && GetComponent<Rigidbody>().velocity.magnitude > maxBreakSpeed/2)
                {
                    GetComponent<AudioSource>().PlayOneShot(allCarSounds[4]);
                    driftSoundReady = false;
                }
                
                GetComponent<Rigidbody>().AddRelativeForce(-Vector3.forward * maxBreakSpeed * Input.GetAxis("LeftBump") * Time.fixedDeltaTime);
            }
            else if (TimeNotMovingForward > 0.4f && GetComponent<Rigidbody>().velocity.magnitude <= maxSpeed/2)
            {
                GetComponent<Rigidbody>().AddRelativeForce(-Vector3.forward * maxAcceleration * Input.GetAxis("LeftBump") * Time.fixedDeltaTime);
            }

        }

        //ready for next drift sound if break is released
        if (Input.GetAxis("LeftBump") < 0.05f && driftSoundReady == false)
            driftSoundReady = true;

        //look sideways
        float newYAngle = Input.GetAxis("RightX") * sideLookClip + startLookAngle.y;

        if (newYAngle > oldYangle + maxLookSpeed * Time.deltaTime)
            newYAngle = oldYangle + maxLookSpeed * Time.deltaTime;

        if (newYAngle < oldYangle - maxLookSpeed * Time.deltaTime)
            newYAngle = oldYangle - maxLookSpeed * Time.deltaTime;

        
        Camera.main.transform.localRotation = Quaternion.Euler(startLookAngle.x, newYAngle, startLookAngle.z);
        oldYangle = newYAngle;

    }
    

    
}

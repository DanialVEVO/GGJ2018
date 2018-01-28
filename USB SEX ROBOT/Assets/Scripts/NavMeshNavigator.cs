﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigator : MonoBehaviour
{
    public static float maxDistance = 99.0f;
    public static float minDistanceNextPoint = 20.0f;
    public static float maxPathTime = 45.0f;
    public static float deathRadius = 20.0f;
    public static float exitPanicModeRadius = 20.0f;
    public static float carPanicRadius = 15.0f;

    public float baseSpeed = 1.5f;
    public float panicSpeed = 3.0f;
    public bool isDead = false;
    
    public static List<NavMeshNavigator> listOfNavigators = new List<NavMeshNavigator>();

    // Debug vals
    public bool initialPanic = false;

    // Privates
    private Vector3 targetPosition = new Vector3();
    private float lastPickTime = 0.0f;
    private bool needsReset = false;
    private int targetMask = 0;

    public bool Panicked
    {
        get
        {
            return isPanicked;
        }

        set
        {
            isPanicked = value;

            NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();

            if (isPanicked)
                navMeshAgent.areaMask = 1 | (1 << 3);

            if (value == false)
                needsReset = true;

            // Pick new point
            PickRandomPoint();
        }
    }

    private bool isPanicked = false;

	// Use this for initialization
	void Awake()
    {
        lastPickTime = Time.time;

        lock (listOfNavigators)
            listOfNavigators.Add(this);

        if (initialPanic)
            Panicked = true;
	}

    void Start()
    {
        PickRandomPoint();
    }

    // Update is called once per frame
    void Update ()
    {
        CheckIfNewPointRequired();

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        if (cars.Length != 0)
        {
            float carDistance = 9999.0f;
            int outOfRadius = 0;

            foreach (GameObject car in cars)
            {
                float curCarDistance = Vector3.Distance(car.transform.position, this.gameObject.transform.position);
                carDistance = Mathf.Min(carDistance, curCarDistance);

                if (Panicked && curCarDistance >= exitPanicModeRadius)
                    outOfRadius++;
            }

            if (Panicked && outOfRadius == cars.Length)
                Panicked = false;
            else if (!Panicked)
            {
                foreach (GameObject car in cars)
                {
                    // Check if car is on sidewalk, and if so, check if we need to panic
                    NavMeshHit hit;
                    bool gotHit = NavMesh.SamplePosition(car.transform.position, out hit, 2.0f, NavMesh.AllAreas);

                    if (gotHit && carDistance <= carPanicRadius)
                    {
                        if ((hit.mask & 1) > 0)
                        {
                            Panicked = true;
                            break;
                        }
                    }
                }
            }
        }

    }


    // Called on death
    private void OnDestroy()
    {
        if (isDead)
        {
            lock (listOfNavigators)
            {
                listOfNavigators.Remove(this);

                // Put others in panic mode
                foreach (NavMeshNavigator navigator in listOfNavigators)
                {
                    if (Vector3.Distance(this.gameObject.transform.position, navigator.gameObject.transform.position) <= deathRadius)
                        navigator.Panicked = true;
                }
            }
        }

        // Find spawner
        GameObject gameObj = GameObject.FindGameObjectWithTag("Spawner");

        if (gameObj != null)
        {
            PerpSpawner perpSpawner = gameObj.GetComponentInChildren<PerpSpawner>();
            perpSpawner.RemoveFromActive(this.gameObject);
        }
    }


    // Get random point
    public void PickRandomPoint()
    {
        NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
            return;

        float minDistance = minDistanceNextPoint * (Panicked ? 2.0f : 1.0f);
        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        // Max 100 iterations
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
            randomDirection += transform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 2.0f, Panicked ? (1 | (1 << 3)) : 1);

            if ((hit.mask & navMeshAgent.areaMask) == 0)
                continue;

            if (Vector3.Distance(this.gameObject.transform.position, hit.position) < minDistance)
            {
                minDistance -= 1.0f;
                minDistance = Mathf.Max(0.0f, minDistance);
                continue;
            }

            bool needsContinue = false;

            foreach (GameObject car in cars)
            {
                if (Vector3.Distance(car.transform.position, hit.position) < minDistance)
                {
                    minDistance -= 1.0f;
                    minDistance = Mathf.Max(0.0f, minDistance);
                    needsContinue = true;
                    break;
                }
            }

            if (needsContinue)
                continue;

            targetPosition = hit.position;
            targetMask = hit.mask;
            break;
        }

        lastPickTime = Time.time;
        navMeshAgent.destination = targetPosition;
        navMeshAgent.speed = Panicked ? panicSpeed : baseSpeed;
    }

    private void CheckIfNewPointRequired()
    {
        if (Vector3.Distance(this.gameObject.transform.position, targetPosition) < 5.0f ||
            (Time.time - lastPickTime) > maxPathTime)
        {
            if (needsReset && (targetMask & 1) > 0)
            {
                NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
                needsReset = false;

                navMeshAgent.areaMask = 1;
            }

            PickRandomPoint();
        }
    }
}

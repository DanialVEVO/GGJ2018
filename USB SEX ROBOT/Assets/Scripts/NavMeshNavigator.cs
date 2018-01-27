using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigator : MonoBehaviour
{
    public static float maxDistance = 99.0f;
    public static float minDistanceNextPoint = 20.0f;
    public static float maxPathTime = 45.0f;

    public float baseSpeed = 2.0f;
    public float panicSpeed = 4.0f;

    public GameObject car = null;

    // Debug vals
    public bool initialPanic = false;

    // Privates
    private Vector3 targetPosition = new Vector3();
    private float lastPickTime = Time.time;

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

            // Reset masks
            navMeshAgent.areaMask = 1;

            if (isPanicked)
                navMeshAgent.areaMask |= (1 << 3);

            // Pick new point
            PickRandomPoint();
        }
    }

    private bool isPanicked = false;

	// Use this for initialization
	void Start ()
    {
        if (initialPanic)
            Panicked = true;

        PickRandomPoint();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfNewPointRequired();
    }


    // Get random point
    public void PickRandomPoint()
    {
        NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (navMeshAgent == null)
            return;

        float minDistance = minDistanceNextPoint * (Panicked ? 2.0f : 1.0f);

        // Max 100 iterations
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
            randomDirection += transform.position;

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 2.0f, navMeshAgent.areaMask);

            if ((hit.mask & navMeshAgent.areaMask) == 0)
                continue;

            if (Vector3.Distance(this.gameObject.transform.position, hit.position) < minDistance)
            {
                minDistance -= 1.0f;
                minDistance = Mathf.Max(0.0f, minDistance);
                continue;
            }

            // Hey dit is retarded code maar ik ben te moe om te refactoren, later misschien OK
            if (car != null && Vector3.Distance(car.transform.position, hit.position) < minDistance)
            {
                minDistance -= 1.0f;
                minDistance = Mathf.Max(0.0f, minDistance);
                continue;
            }

            targetPosition = hit.position;
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
            PickRandomPoint();
    }
}

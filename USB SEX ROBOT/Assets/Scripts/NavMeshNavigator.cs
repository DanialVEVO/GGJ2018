using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshNavigator : MonoBehaviour
{
    public static float maxDistance = 99.0f;
    public static float minDistanceNextPoint = 20.0f;
    public static float maxPathTime = 45.0f;
    public static float deathRadius = 10.0f;
    public static float exitPanicModeRadius = 20.0f;
    public static float carPanicRadius = 10.0f;

    public float baseSpeed = 2.0f;
    public float panicSpeed = 4.0f;

    public GameObject car = null;
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
	void Start ()
    {
        lastPickTime = Time.time;

        lock (listOfNavigators)
            listOfNavigators.Add(this);

        if (initialPanic)
            Panicked = true;

        PickRandomPoint();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckIfNewPointRequired();

        if (car != null)
        {
            float carDistance = Vector3.Distance(car.transform.position, this.gameObject.transform.position);

            if (Panicked && carDistance >= exitPanicModeRadius)
                Panicked = false;

            if (!Panicked)
            {
                // Check if car is on sidewalk, and if so, check if we need to panic
                NavMeshHit hit;
                bool gotHit = NavMesh.SamplePosition(car.transform.position, out hit, 2.0f, NavMesh.AllAreas);

                if (gotHit && carDistance <= carPanicRadius)
                {
                    if ((hit.mask & 1) > 0)
                        Panicked = true;
                }
            }
        }

    }


    // Called on death
    private void OnDestroy()
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
            NavMesh.SamplePosition(randomDirection, out hit, 2.0f, Panicked ? (1 | (1 << 3)) : 1);

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

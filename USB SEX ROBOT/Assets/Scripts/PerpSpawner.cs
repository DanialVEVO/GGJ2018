using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PerpSpawner : MonoBehaviour
{
    // Pool of all perps
    public int MaxNumberOfPerps = 20;
    public GameObject[] ListOfPossiblePerps;
    public float SpawnMinDistanceFromCars = 30.0f;

    // Bookkeeping
    private List<GameObject> activeSuspectablePerps = new List<GameObject>();
    private List<GameObject> activeInnocents = new List<GameObject>();
    private bool flipFlopSuspectInnocent = false;

    private List<GameObject> possibleSuspects = new List<GameObject>();
    private List<GameObject> possibleInnocents = new List<GameObject>();
    private object lockObject = new object();

    // Use this for initialization
    void Start ()
    {
        // Divide the lists
        foreach (GameObject gameObj in ListOfPossiblePerps)
        {
            PerpDescription perpDescription = gameObj.GetComponent<PerpDescription>();

            if (perpDescription != null)
            {
                if (perpDescription.suspect)
                    possibleSuspects.Add(gameObj);
                else
                    possibleInnocents.Add(gameObj);
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        lock (lockObject)
            SpawnNpcs();
    }

    public void RemoveFromActive(GameObject perp)
    {
        lock (lockObject)
        {
            activeSuspectablePerps.Remove(perp);
            activeInnocents.Remove(perp);
        }
    }

    void SpawnNpcs()
    {
        int numActive = activeInnocents.Count + activeSuspectablePerps.Count;
        int numPossibleSuspects = possibleSuspects.Count;
        int numPossibleInnocents = possibleInnocents.Count;

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

        for (int i = numActive; i < MaxNumberOfPerps; i++)
        {
            GameObject newPerp = null;
            bool isSuspect = false;

            int b = 0;
            for (b = 0; b < 10; b++)
            {
                int suspectIndex = Random.Range(0, numPossibleSuspects);
                int innocentIndex = Random.Range(0, numPossibleInnocents);

                GameObject perp = null;

                if (flipFlopSuspectInnocent)
                    perp = possibleSuspects[suspectIndex];
                else
                    perp = possibleInnocents[innocentIndex];

                newPerp = Instantiate(perp, new Vector3(0.0f, -15.0f, 0.0f), Quaternion.identity);
                bool twinResolved = ResolveIdenticalSuspect(newPerp, flipFlopSuspectInnocent);

                newPerp.transform.parent = this.transform;

                if (!twinResolved)
                {
                    Destroy(newPerp);
                    continue;
                }

                isSuspect = flipFlopSuspectInnocent;
                flipFlopSuspectInnocent = !flipFlopSuspectInnocent;
                break;
            }

            // Error case
            if (b == 10)
                return;

            // Find a spawning position
            // 100 iterations max
            NavMeshHit hit = new NavMeshHit();

            for ( b = 0; b < 10; b++)
            {
                Vector3 randomDirection = Random.insideUnitSphere * 100.0f;
                NavMesh.SamplePosition(randomDirection, out hit, 200.0f, 1);

                bool notGoodEnough = false;

                foreach (GameObject car in cars)
                {
                    if (Vector3.Distance(car.transform.position, hit.position) < SpawnMinDistanceFromCars)
                    {
                        notGoodEnough = true;
                        break;
                    }
                }

                if (notGoodEnough == false)
                    break;
            }

            // Error case
            if (b == 10)
                return;

            // We've got a spawn position
            NavMeshAgent agent = newPerp.GetComponentInChildren<NavMeshAgent>();
            Debug.Assert(agent != null);
            agent.Warp(hit.position);
            newPerp.transform.position = hit.position + new Vector3(0, 8, 0);

            if (isSuspect)
                activeSuspectablePerps.Add(newPerp);
            else
                activeInnocents.Add(newPerp);
        }
    }

    bool ResolveIdenticalSuspect(GameObject perp, bool isSuspect)
    {
        if (!isSuspect)
            return true;

        bool hasIdenticalTwin = false;
        PerpDescription desc2 = perp.GetComponent<PerpDescription>();

        string spriteName2 = "";
        SpriteRenderer spriteRenderer2 = perp.GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer2 != null)
            spriteName2 = spriteRenderer2.sprite.name;

        string hash2 = desc2.GetPerpHash(spriteName2);

        foreach (GameObject obj in activeSuspectablePerps)
        {
            PerpDescription desc1 = obj.GetComponentInChildren<PerpDescription>();
            Debug.Assert(desc1 != null, "Cannot have a perp without PerpDescription!");

            // Need to get a sprite renderer of perp
            string spriteName = "";
            SpriteRenderer spriteRenderer = obj.GetComponentInChildren<SpriteRenderer>();

            if (spriteRenderer != null)
                spriteName = spriteRenderer.sprite.name;

            string hash1 = desc1.GetPerpHash(spriteName);

            if (hash1 == hash2)
                hasIdenticalTwin = true;
        }

        return !hasIdenticalTwin;
    }
}

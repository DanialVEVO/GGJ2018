using UnityEngine;
using System.Collections;

public class YodlDetector : MonoBehaviour
{
    public GameObject yodlNode;

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PerpDescription>())
        {
            if (!other.GetComponentInChildren<YodlGenerator>())
            {
                Debug.LogWarning("Colliding!");

                Transform colOrigin = other.transform;
                GameObject obj = Instantiate(yodlNode, colOrigin);

                obj.transform.parent = other.transform;
            }
        }
    }
}

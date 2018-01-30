using UnityEngine;
using System.Collections;

public class spin2 : MonoBehaviour
{
    public float speed2 = 100f;


    void Update()
    {
        transform.Rotate(Vector3.right, speed2 * Time.deltaTime);
    }
}
using UnityEngine;
using System.Collections;

public class spin : MonoBehaviour
{
    public float speed = 100f;


    void Update()
    {
        transform.Rotate(Vector3.left, speed * Time.deltaTime);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour {

    public GameObject target;
    public float speed;

	// Update is called once per frame
	void Update ()
    {
        target = GameObject.Find("car");

        Vector3 pos = transform.position;

        Vector3 targetDir = target.transform.position - transform.position;
        float step = speed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}

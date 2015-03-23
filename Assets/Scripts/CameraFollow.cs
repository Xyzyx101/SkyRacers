using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject target;
    public Vector3 offset;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPoint = target.transform.TransformPoint(offset);
        gameObject.transform.position = newPoint;
        gameObject.transform.LookAt(target.transform.position);
	}
}

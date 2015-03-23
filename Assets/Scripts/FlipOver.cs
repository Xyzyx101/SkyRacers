using UnityEngine;
using System.Collections;

public class FlipOver : MonoBehaviour {

    private float reflipTime = 3f;
    private float reflipTimer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (reflipTimer < 0)
        {
            bool flipOver = Input.GetKey(KeyCode.Q);
            if (flipOver)
            {
                transform.position = transform.position + new Vector3(0f, 3f, 0f);
                transform.Rotate(Vector3.forward, 180f);
                reflipTimer = reflipTime;
            }
        }
        reflipTimer -= Time.deltaTime;
	}
}



       
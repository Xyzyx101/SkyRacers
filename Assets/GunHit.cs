using UnityEngine;
using System.Collections;

public class GunHit : MonoBehaviour {
    private float deathTimer;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        deathTimer = 4f;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        deathTimer -= Time.deltaTime;
        audio.pitch = Random.RandomRange(0.5f, 1.5f);
        if (deathTimer < 0)
        {
            Destroy(this.gameObject);
        }
	}
}

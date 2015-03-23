using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    [SerializeField]
    private ParticleSystem[] particles;
    [SerializeField] private float rate;
    private float rateTimer;
    [SerializeField]
    private GameObject hitEffect;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        rateTimer = rate;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        rateTimer -= Time.deltaTime;
	}

    public void Fire()
    {
        if(rateTimer < 0) {
            foreach (ParticleSystem ps in particles)
            {
                ps.Play();
                RaycastHit hitInfo;
                if ( Physics.Raycast(transform.position, transform.forward, out hitInfo)  ) {
                    Quaternion normalRot = new Quaternion();
                    normalRot.eulerAngles = hitInfo.normal;
                    Instantiate(hitEffect, hitInfo.point, normalRot );
                }
            }
            audio.Play();
            rateTimer = rate;
        }
    }
}

using UnityEngine;
using System.Collections;

public class Exhaust : MonoBehaviour {
    [SerializeField]
    private ParticleSystem particleSystem;
    private float engineRPM;
	
        // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        particleSystem.startSpeed = engineRPM / 50;
        particleSystem.emissionRate = engineRPM / 10;
	}

    public void SetEngineInfo(float rpm)
    {
        engineRPM = rpm;
    }
}

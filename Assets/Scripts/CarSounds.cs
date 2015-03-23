using UnityEngine;
using System.Collections;

public class CarSounds : MonoBehaviour {
    [SerializeField] 
    private AudioClip RPM_Wub;
    [SerializeField] 
    private AudioClip ThrottleAttack;
    [SerializeField] 
    private AudioClip ThrottleSustain;
    [SerializeField] 
    private AudioClip ThrottleDecay;
    [SerializeField]
    private float minRPM;
    [SerializeField]
    private float maxRPM;

    private float throttleTimer;
    //This time is the length of the throttle sustain sound
    private float throttleTime = 0.3f;
    private float lastThrottle = 0;
    private float currentThrottle = 0;
    private float currentRPM = 0;

    private AudioSource RPMSource;
    private AudioSource[] ThrottleSources;
    private int ThrottleSourceCounter = 0;

    public void UpdateCarAudio(float throttle, float rpm)
    {
        lastThrottle = currentThrottle;
        currentThrottle = throttle;
        currentRPM = rpm;
    }

    void Start()
    {
        RPMSource = gameObject.AddComponent<AudioSource>();
        RPMSource.clip = RPM_Wub;
        RPMSource.loop = true;
        RPMSource.Play();
        RPMSource.volume = 0.15f;
        RPMSource.pitch = 0.4f;

        ThrottleSources = new AudioSource[4];
        for (uint i = 0; i < ThrottleSources.Length; ++i )
        {
            ThrottleSources[i] = gameObject.AddComponent<AudioSource>();
            ThrottleSources[i].loop = false;
            ThrottleSources[i].volume = 0.3f;
            ThrottleSources[i].pitch = 1f;
        }
    }

    void Update () {
        float RPMPitchFactor = Mathf.Clamp01(currentRPM / maxRPM);
        RPMSource.pitch = RPMPitchFactor * 2.6f + 0.4f;

        PlayThrottleSounds();
        throttleTimer -= Time.deltaTime;
	}

    void PlayThrottleSounds()
    {
        AudioClip clip;
        if (currentThrottle > 0.1)
        {
            if (lastThrottle < 0.1)
            {
                clip = ThrottleAttack;
                throttleTimer = throttleTime;
            }
            else
            {
                if (throttleTimer < 0)
                {
                    clip = ThrottleSustain;
                    throttleTimer = throttleTime;
                }
                else
                {
                    return;
                } 
            }
        } 
        else 
        {
            if (lastThrottle > 0.1)
            {
                clip = ThrottleDecay;
            }
            else
            {
                return;
            }
        }
        AudioSource throttleSource = ThrottleSources[++ThrottleSourceCounter % ThrottleSources.Length];
        throttleSource.PlayOneShot(clip);
    } 
}

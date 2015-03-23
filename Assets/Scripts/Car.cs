using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {
    [SerializeField] private WheelCollider[] driveWheels;
    [SerializeField] private WheelCollider[] steerWheels;
    [SerializeField] private WheelCollider[] handBrakeWheels;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private float maxEngineTorque;
    [SerializeField] private float maxBrakeTorque;
    [SerializeField] private float maxHandBrakeTorque;

    [SerializeField] private float maxRPM;
    [SerializeField] private float rpm;
    [SerializeField] private float torqueCurveExp;
    [SerializeField] private float rpmDecay;
    
    [SerializeField] private float currentThrottle;

    [SerializeField] private float currentSteerAngle;
    [SerializeField] private float currentBrake;
    [SerializeField] private bool handbrakeApplied;

    [SerializeField] private float DownAccel;

    private Rigidbody rigidbody;
    private WheelCollider[] wheelColliders;
    private GameObject[] wheelMesh = new GameObject[4];
    private CarSounds carSounds;
    private CarGui carGUI;
    private Exhaust exhaust;

	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        wheelColliders = GetComponentsInChildren<WheelCollider>();
        for (uint i = 0; i < 4; ++i) {
            MeshRenderer mesh = wheelColliders[i].gameObject.GetComponentInChildren<MeshRenderer>();
            wheelMesh[i] = mesh.gameObject;
        }
        carSounds = GetComponent<CarSounds>();
        carGUI = GetComponent<CarGui>();
        GameObject com = GameObject.Find("COM");
        if (com)
        {
            rigidbody.centerOfMass = com.transform.localPosition;
        }
        exhaust = GetComponent<Exhaust>();
    }
	
    void Update () {
        UpdateWheelMeshTransform();

        steerWheels[0].steerAngle = currentSteerAngle;
        steerWheels[1].steerAngle = currentSteerAngle;

        // apply engine torque
        float wheelTorque = maxEngineTorque * (rpm / maxRPM) / driveWheels.Length;
        for ( uint i = 0; i < driveWheels.Length; ++i )
        {
            driveWheels[i].motorTorque = wheelTorque;
        }

        float brakeTorque = maxBrakeTorque * currentBrake / wheelColliders.Length;
        for (uint i = 0; i < wheelColliders.Length; ++i)
        {
            wheelColliders[i].brakeTorque = brakeTorque;
        }
        if (handbrakeApplied)
        {
            for (uint i = 0; i < handBrakeWheels.Length; ++i)
            {
                handBrakeWheels[i].brakeTorque = maxHandBrakeTorque;
            }
        }
        UpdateRPM();
        carSounds.UpdateCarAudio(currentThrottle, rpm);
        carGUI.SetSpeed(Vector3.Dot(rigidbody.velocity, transform.forward));
        exhaust.SetEngineInfo(rpm);
	}

    void FixedUpdate()
    {
        bool isGrounded = true;
        foreach (WheelCollider wheel in wheelColliders)
        {
            WheelHit hit;
            if ( !wheel.GetGroundHit(out hit) ) {
                isGrounded = false;
            }
        }
        if (isGrounded) {
            Debug.Log("downforce");
            rigidbody.AddForce(transform.up * -DownAccel);
        }
        else
        {
            Debug.Log("no downforce");
        }
    }

    public void SetInputs (float throttleAxis, float brakeAxis, float steerAxis, bool handbrake) {
        currentSteerAngle = steerAxis * maxSteerAngle;
        currentThrottle = throttleAxis;
        currentBrake = brakeAxis;
        handbrakeApplied = handbrake;
    }

    void UpdateRPM()
    {
        if (currentThrottle > 0.1)
        {
            float deltaRPM = Mathf.Pow((maxRPM / rpm),torqueCurveExp);
            deltaRPM *= currentThrottle;
            rpm += deltaRPM;
        }
        else
        {
            rpm *= rpmDecay;
        }
        rpm = Mathf.Clamp(rpm, 100f, maxRPM);
    }

    // Wheel mesh is only cosmetic
    void UpdateWheelMeshTransform()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            Quaternion quat;
            Vector3 position;
            wheelColliders[i].GetWorldPose(out position, out quat);
            wheelMesh[i].transform.position = position;
            wheelMesh[i].transform.rotation = quat;
        }
    }
}

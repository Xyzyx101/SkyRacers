using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public float throttleAxis;
    public float throttleMax;
    public float throttleAttack;
    public float throttleDecay;

    public float brakeAxis;
    public float brakeMax;
    public float brakeAttack;
    public float brakeDecay;
    
    public float steerAxis;
    public float steerMax;
    public float steerAttack;
    public float steerDecay;
    public float steerCorrection;

    public int gear;

    private Car car;
    private Gun[] guns;

	void Start () {
        car = GetComponent<Car>();
        guns = GetComponentsInChildren<Gun>();
	}
	
	void Update () {
        
	}

    void FixedUpdate()
    {
        SetThrottle(Input.GetKey(KeyCode.W));
        SetBrake(Input.GetKey(KeyCode.S));
        SetSteer(Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));
        bool handbrake = Input.GetKey(KeyCode.Space);
        car.SetInputs(throttleAxis, brakeAxis, steerAxis, handbrake);
        if (Input.GetKey(KeyCode.RightShift))
        {
            foreach (Gun gun in guns)
            {
                gun.Fire();
            }
        }
    }


    void SetThrottle (bool forward)
    {
        if (forward)
        {
            throttleAxis *= throttleAttack;
            throttleAxis = Mathf.Max(throttleAxis, 0.1f);
            throttleAxis = Mathf.Min(throttleAxis, throttleMax);
        }
        else
        {
            throttleAxis *= throttleDecay;
            if (throttleAxis < 0.1)
            {
                throttleAxis = 0;
            }
        }
    }

    void SetBrake(bool back)
    {
        if (back)
        {
            brakeAxis *= brakeAttack;
            brakeAxis = Mathf.Max(brakeAxis, 0.1f);
            brakeAxis = Mathf.Min(brakeAxis, brakeMax);
        }
        else
        {
            brakeAxis *= brakeDecay;
            if (brakeAxis < 0.1)
            {
                brakeAxis = 0;
            }
        }
    }
    void SetSteer(bool left, bool right)
    {
        if (steerAxis > 0) // turning right
        {
            if (right)
            {
                steerAxis *= steerAttack;
                //steerAxis = Mathf.Max(steerAxis, 0.01f);
                steerAxis = Mathf.Min(steerAxis, steerMax);
            }
            else
            {
                steerAxis *= steerDecay;
                if (steerAxis < 0.01)
                {
                    steerAxis = 0f;
                }
            }
            if (left) {
                steerAxis *= steerCorrection;
            }
        }
        else if (steerAxis < 0) // turning left
        {
            float tempSteerAxis = Mathf.Abs(steerAxis);
            if (left)
            {
                tempSteerAxis *= steerAttack;
                //tempSteerAxis = Mathf.Max(tempSteerAxis, 0.01f);
                tempSteerAxis = Mathf.Min(tempSteerAxis, steerMax);
            }
            else
            {
                tempSteerAxis *= steerDecay;
                if (tempSteerAxis < 0.01)
                {
                    tempSteerAxis = 0f;
                }
            }
            if (right)
            {
                tempSteerAxis *= steerCorrection;
            }
            steerAxis = -tempSteerAxis;
        }
        else
        {
            if (right)
            {
                steerAxis += 0.01f;
            }
            if (left)
            {
                steerAxis -= 0.01f;
            }
        }
    }
}

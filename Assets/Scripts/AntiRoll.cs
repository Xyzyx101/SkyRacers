﻿using UnityEngine;
using System.Collections;

public class AntiRoll : MonoBehaviour {
    [SerializeField] private WheelCollider wheelR;
    [SerializeField] private WheelCollider wheelL;
    [SerializeField] private float antiRoll;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = wheelL.GetGroundHit(out hit);
        if (groundedL)
        {
            travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) / wheelL.suspensionDistance;
        }
        bool groundedR = wheelR.GetGroundHit(out hit);
        if (groundedR)
        {
            travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) / wheelR.suspensionDistance;
        }

        float antiRollForce = (travelL - travelR) * antiRoll;
        if ( groundedL ) {
            rigidbody.AddForceAtPosition(wheelL.transform.up * -antiRollForce, wheelL.transform.position);
            //Debug.DrawRay(wheelL.transform.position, wheelL.transform.up * antiRollForce);
        }
        if ( groundedR ) {
            rigidbody.AddForceAtPosition(wheelR.transform.up * antiRollForce, wheelR.transform.position);
            //Debug.DrawRay(wheelR.transform.position, wheelR.transform.up * -antiRollForce);
        }
    }
}

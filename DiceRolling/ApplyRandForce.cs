using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyRandForce : MonoBehaviour {

    public string buttonName = "Fire1";
    public float forceAmount = 10.0f;
    public float torqueAmount = 10.0f;
    public ForceMode forceMode;
    public float maxAngularVelocity;
    public Rigidbody rb;

    void Start() {

        rb.maxAngularVelocity = maxAngularVelocity;
    }

    void FixedUpdate ()
    {
		if(Input.GetButtonDown(buttonName))
        {
            rb.AddForce(Random.onUnitSphere*forceAmount,forceMode); 
            rb.AddTorque(Random.onUnitSphere * torqueAmount, forceMode);
        }
	}
}

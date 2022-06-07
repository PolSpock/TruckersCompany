using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerManagementScript : MonoBehaviour {

    public Vector3 gravityCenter;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = gravityCenter;
    }

    // Update is called once per frame
    void Update () {
		
	}
}

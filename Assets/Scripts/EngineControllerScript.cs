using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class MainWheels : System.Object
{
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
	public bool reverseTurn; 
}

[System.Serializable]
public class BackWheels : System.Object
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public GameObject wheelsMesh;
}

public class EngineControllerScript : MonoBehaviour {

	public float maxMotorTorque;
	public float maxSteeringAngle;
	public List<MainWheels> truck_Main;
    public List<BackWheels> truck_Back;

    public void VisualizeWheel(MainWheels wheelPair)
	{
		Quaternion rot;
		Vector3 pos;
		wheelPair.leftWheel.GetWorldPose ( out pos, out rot);
		wheelPair.leftWheelMesh.transform.position = pos;
		wheelPair.leftWheelMesh.transform.rotation = rot;
		wheelPair.rightWheel.GetWorldPose ( out pos, out rot);
		wheelPair.rightWheelMesh.transform.position = pos;
		wheelPair.rightWheelMesh.transform.rotation = rot;
	}

    public void Update()
	{
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
		float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
		float brakeTorque = Mathf.Abs(Input.GetAxis("Jump"));

        // Freinage
        if (brakeTorque > 0.001) {
			brakeTorque = maxMotorTorque * 10;
			motor = 0;
		} else {
			brakeTorque = 0;
		}

        // Roues directionnelles
		foreach (MainWheels truck_Info in truck_Main)
		{
			if (truck_Info.steering == true) {
				truck_Info.leftWheel.steerAngle = truck_Info.rightWheel.steerAngle = ((truck_Info.reverseTurn)?-1:1)*steering;
			}

			if (truck_Info.motor == true)
			{
				truck_Info.leftWheel.motorTorque = motor;
				truck_Info.rightWheel.motorTorque = motor;
			}

			truck_Info.leftWheel.brakeTorque = brakeTorque;
			truck_Info.rightWheel.brakeTorque = brakeTorque;

			VisualizeWheel(truck_Info);
		}

        foreach (BackWheels truck_Info in truck_Back)
        {
            truck_Info.leftWheel.brakeTorque = brakeTorque;
            truck_Info.rightWheel.brakeTorque = brakeTorque;
        }

    }


}
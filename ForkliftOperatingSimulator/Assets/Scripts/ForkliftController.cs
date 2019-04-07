using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
	This code was downloaded from the Unity asset store and is the work of DotTeam.
	It is available at the following: https://assetstore.unity.com/packages/tools/physics/car-script-basic-61615
	And you can view their other assets here: https://assetstore.unity.com/publishers/16180
	
	I have made small changes to adapt the script for my project. These include:
	1) Adding OVRInput to map controls to the oculus controller
	2) Added comments everywhere to show I have read and understand the code completely
	3) Changed any variable name with 'truck' in it to 'forklift'
	
	-Simon O'Leary
*/



[System.Serializable]
public class Forklift : System.Object
{
	public WheelCollider leftWheel;
	public GameObject leftWheelMesh;
	public WheelCollider rightWheel;
	public GameObject rightWheelMesh;
	public bool motor;
	public bool steering;
	public bool reverseTurn; 
}

public class ForkliftController : MonoBehaviour {

	public float maxMotorTorque;
	public float maxSteeringAngle;
	public List<Forklift> forklift_Infos;

    public SteeringWheelOutPut steeringWheelOutPut;
    public ControlsManager controlsManagerR;
    public ControlsManager controlsManagerL;

    public float accel = 0;
    float brakeTorque = 0;

    /* More SteamVR variables
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }
    */

    //This method is called in update to reflect the user steering (turns the wheels)
    public void VisualizeWheel(Forklift wheelPair)
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

    public float map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    public void Update()
	{
        //Right grip button held down
        if(controlsManagerR.rtriggerpulled)
        {
            accel = 1;
        }
        else
        {
            accel = 0;
        }

        //left grip button held down
        if (controlsManagerL.ltriggerpulled)
        {
            brakeTorque = 1;
        }
        else
        {
            brakeTorque = 0;
        }

        float motor = maxMotorTorque * accel; //Acceleration is controlled by squeezing the right grip button

        // map steering wheel rotation and multiply here instead of horizontal //Input.GetAxis("Horizontal");
        float steering = maxSteeringAngle * 
            map(-360, 360, -1, 1, steeringWheelOutPut.outAngle); //returns value between -1 and 1

        
		//Disables motor when handbrake is used
		if (brakeTorque > 0.001) {
			brakeTorque = maxMotorTorque;
			motor = 0;
		} else {
			brakeTorque = 0;
		}

		foreach (Forklift forklift_info in forklift_Infos)
		{
			//When the user is turning
			if (forklift_info.steering == true) {
				forklift_info.leftWheel.steerAngle = forklift_info.rightWheel.steerAngle = ((forklift_info.reverseTurn)?-1:1)*steering;
			}

			//when the user is accelerating
			if (forklift_info.motor == true)
			{
				forklift_info.leftWheel.motorTorque = motor;
				forklift_info.rightWheel.motorTorque = motor;
			}
			
			forklift_info.leftWheel.brakeTorque = brakeTorque;
			forklift_info.rightWheel.brakeTorque = brakeTorque;

			//call the VisualizeWheel function declared above which alters the appearance of the wheel
			VisualizeWheel(forklift_info);
		}

	}


}
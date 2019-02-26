using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringTest : MonoBehaviour
{
	
	//Is wheel held or not (false by default)
	public bool held = false;
	
	
	////public SteeringWheelOutPut steeringWheelOutPut;
	
	//SteeringWheel Relative Point
	public GameObject StWheel;
	public GameObject Hand;
	
	//Hand and wheel rel position
	public Vector3 RelativePos;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	
	float CalculateRawAngle()
    {
        RelativePos = StWheel.transform.InverseTransformPoint(Hand.transform.position); // Relative position between the wheel and hand
        float angle = Mathf.Atan2(RelativePos.x, RelativePos.z);
        //return Mathf.Atan2(RelativePos.y, RelativePos.x) * Mathf.Rad2Deg; // ATan2 gives radians and multiplying by Rad2Deg returns the value in degrees
        return angle; //* Mathf.Rad2Deg; // ATan2 gives radians and multiplying by Rad2Deg returns the value in degrees
    }
	
	private float prevAngle;
    private Quaternion prevQuat;

    // Update is called once per frame
    void Update()
    {
		OVRInput.Update();
		
       
    }//end update()

    public void OnTriggerExit(Collider other)
    {
        held = false;
    }

    public void OnTriggerStay(Collider target)
	{			
		if(target.tag == "Hand") //when hands enter the steering wheel collider
		{
            //Debug.Log("Grabbable");			
            if ((OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)) > 0.5 || (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)) > 0.5)
            {
                if (!held)
                {
                    prevAngle = CalculateRawAngle();
                    held = true;
                }
                else
                {
                    float Angle = CalculateRawAngle();
                    Debug.Log(Angle);
                    float AngleDelta = Angle - prevAngle;
                    //aternion q = Quaternion.AngleAxis(AngleDelta * Mathf.Rad2Deg, transform.up);
                    //transform.rotation = q*transform.rotation;
                    transform.Rotate(0, AngleDelta * Mathf.Rad2Deg, 0);
                    prevAngle = Angle;
                }
            }
            else
            {
                held = false;
            }
		}

	}//end onTriggerStay()

	
	
}//end main()

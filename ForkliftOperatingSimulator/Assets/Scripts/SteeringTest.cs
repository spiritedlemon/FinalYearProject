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
        
        return Mathf.Atan2(RelativePos.y, RelativePos.x) * Mathf.Rad2Deg; // ATan2 gives radians and multiplying by Rad2Deg returns the value in degrees
    }
	

    // Update is called once per frame
    void Update()
    {
		OVRInput.Update();
		
		//steeringWheelOutPut.outAngle = outputAngle;
        float angle;
        if (held)
        {
            angle = CalculateRawAngle(); // When hands are holding the wheel, hand dictates how the wheel moves
			Debug.Log(angle); //Printing 5.8 at 0 and 11.6 at 80 degrees
        }
		
		
		
		//Rotate wheel around Y axis
		//transform.localEulerAngles = 
		
		
		//Rotate with hand movement if grabbed == true
		/*while(held == true)
		{
			
		}
		*/
        
    }//end update()
	
	
	void OnTriggerStay(Collider target)
	{
		if(target.tag == "Hand") //when hands enter the steering wheel collider
		{
			//Debug.Log("Grabbable");
			
			//if trigger is pulled (Axis1d returns a float of 0.0 to 1.0 
			//if it's more than half pulled the folling will trigger
			while( (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)) > 0.5)
			{
				
				if (held != true)
				{
					//save controller position?
					
					held = true;
				}
			}//end while
			
			//If trigger is not pulled in held is set to false
			while( (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)) < 0.5)
			{
				
				if (held != false)
				{
					
					held = false;
					
				}
			}//end while
			
			//if trigger is pulled (Axis1d returns a float of 0.0 to 1.0
			//if it's more than half pulled the folling will trigger
			while( (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)) > 0.5)
			{
				
				if (held != true)
				{
					//save controller position?
					
					held = true;
				}
				
			}//end while
			
			//If trigger is not pulled in held is set to false
			while( (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)) < 0.5)
			{
				
				if (held != false)
				{
					
					held = false;
					
				}
				
			}//end while
		}
		
	}//end onTriggerStay()
	
	
	
}//end main()

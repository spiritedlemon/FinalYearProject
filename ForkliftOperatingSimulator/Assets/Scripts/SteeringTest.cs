using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringTest : MonoBehaviour
{
	
	////private IEnumerator held1;
	
	//Is wheel held or not (false by default)
	public bool held = false;
	
	//Offset between wheel rotation and hand position on grab
	private float angleStickyOffset; 
	
	////public SteeringWheelOutPut steeringWheelOutPut;
	
	//SteeringWheel Relative Point
	public GameObject WheelBase;
	public GameObject Hand;
	
	//Hand and wheel rel position
	public Vector3 RelativePos;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	/*
	void CalculateOffset()
    {
        float rawAngle = CalculateRawAngle();
        angleStickyOffset = outputAngle - rawAngle;
    }
	*/
	
	float CalculateRawAngle()
    {
        RelativePos = WheelBase.transform.InverseTransformPoint(Hand.transform.position); // GETTING RELATIVE POSITION BETWEEN STEERING WHEEL BASE AND HAND
        
        return Mathf.Atan2(RelativePos.y, RelativePos.x) * Mathf.Rad2Deg; // GETTING CIRCULAR DATA FROM X & Y RELATIVES  VECTORS
    }
	

    // Update is called once per frame
    void Update()
    {
		OVRInput.Update();
		
		//steeringWheelOutPut.outAngle = outputAngle;
        float angle;
        if (held)
        {
            angle = CalculateRawAngle(); // + angleStickyOffset; // When hands are holding the wheel hand dictates how the wheel moves
			Debug.Log(angle);
            // angleSticky Offset is calculated on wheel grab - makes wheel not to rotate instantly to the users hand
        }
		
		
		
		//outputAngle = hookedAngles(angle);// SETTING OUTPUT THROUGH FUNCTION
		//Rotate wheel around Y axis
		//transform.localEulerAngles = new Vector3(outputAngle+90, -90, -90); 
		
		
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
	
	/*
	private IEnumerator held()
	{
		
		Debug.Log("In Thread");
		
		//return 0;
		
	}
	*/
	
	
}//end main()

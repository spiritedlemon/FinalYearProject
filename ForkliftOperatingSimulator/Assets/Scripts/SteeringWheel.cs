using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
	
	//private IEnumerator held1;
	public bool held = false;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		OVRInput.Update();
		
		
		
		
		//Rotate with hand movement if grabbed == true
		/*while(held == true)
		{
			
		}
		*/
        
    }
	/*
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
	*/
	/*
	private IEnumerator held()
	{
		
		Debug.Log("In Thread");
		
		//return 0;
		
	}
	*/
	
	
}//end main()

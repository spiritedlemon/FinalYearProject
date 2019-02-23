using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
	
	//private IEnumerator held1;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		OVRInput.Update();
		
		//Set limit for rotation
		if(transform.rotation >= 180)
		{
			transform.rotation = 180;
		}
		if(transform.rotation <= -180)
		{
			transform.rotation = -180;
		}
		
		
		
		//Rotate with hand movement if grabbed == true
        
    }
	
	void OnTriggerStay(Collider target)
	{
		if(target.tag == "Hand")
		{
			//Debug.Log("Grabbable");
			
			//if trigger is pulled (Axis1d returns a float of 0.0 to 1.0 so if it's more than half pulled the folling will trigger
			if( (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)) > 0.5)
			{
				//StartCoroutine(held1);
				//Debug.Log("StartCoroutine");
			}
			
			//if trigger is pulled (Axis1d returns a float of 0.0 to 1.0 so if it's more than half pulled the folling will trigger
			if( (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)) > 0.5)
			{
				//StartCoroutine(held1);
				//Debug.Log("StartCoroutine");
			}
		}
	}
	
	/*
	private IEnumerator held()
	{
		
		Debug.Log("Did I do a thing??");
		
		//return 0;
		
	}
	*/
	
	
}

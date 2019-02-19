using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlLever : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Set limit for rotation
		//Rotate with hand movement if grabbed == true
		
    }
	
	
	void OnCollisionStay(Collision CollisionInfo)
	{
		if(CollisionInfo.gameObject.tag == "Hand")
		{
			Debug.Log("grabbable");
			//if trigger pressed set grabbed to true
			
		}
	}
}

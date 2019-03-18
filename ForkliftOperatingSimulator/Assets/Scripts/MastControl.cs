using UnityEngine;
using System.Collections;

public class MastControl : MonoBehaviour {

    public Transform fork; 
    public Transform mast;
    public float speedTranslate; //Speed at which the fork raises
	
	public Vector3 maxYmast; //The maximum height of the mast
    public Vector3 minYmast; //The minimum height of the mast
	
    public Vector3 maxY; //The maximum height of the platform
    public Vector3 minY; //The minimum height of the platform
	
	//Min and max of moving forks sideways
	public Vector3 sideMax;
	public Vector3 sideMin;
	
    

    private bool mastMoveTrue = false; //Whether or not you want to allow the mast movement
	
	

    // Update is called once per (fixed) frame
    void FixedUpdate () {
		
		OVRInput.Update();
		
		//if fork not already at max height
        if(fork.transform.position.y >= maxYmast.y)
        {
            mastMoveTrue = true; //allows mast to move
        }
		//If fork already at max height
        if (fork.transform.position.y <= maxYmast.y)
        {
            mastMoveTrue = false;
        }
		
		
		//These are for raising/lowering the fork
        if (fork.transform.position.y >= maxY.y )
        {
			//Stops the fork from continuing off the top of the mast
            fork.transform.position = new Vector3(fork.transform.position.x, maxY.y, fork.transform.position.z);
        }

        if (fork.transform.position.y <= minY.y)
        {
			//Stops fork from moving off the bottom of the mast
            fork.transform.position = new Vector3(fork.transform.position.x, minY.y, fork.transform.position.z);
        }
		
		
		//These are for raising/lowering the mast
		//if mast already at max height
        if(mast.transform.position.y <= minYmast.y)
        {
            mast.transform.position = new Vector3(mast.transform.position.x, minYmast.y, mast.transform.position.z);
        }
		//if mast not already at max height
        if(mast.transform.position.y >= maxYmast.y)
        {
            mast.transform.position = new Vector3(mast.transform.position.x, maxYmast.y, mast.transform.position.z);
        }
		
		/*
		//These are for shifting the fork left/right
		if (fork.transform.position.x >= sideMax.x )
        {
			//Stops the fork from continuing off the side of the mast
            fork.transform.position = new Vector3(sideMax.x, fork.transform.position.y, fork.transform.position.z);
        }
		
        if (fork.transform.position.x <= sideMin.x)
        {
			//Stops fork from moving off the other side of the mast
           fork.transform.position = new Vector3(sideMin.x, fork.transform.position.y, fork.transform.position.z);
        }
		*/
		
		
		//'-' key lowers fork & mast 
        //if(Input.GetKey(KeyCode.Minus))
		if(OVRInput.Get(OVRInput.Button.Three))
        {
			//Debug.Log("Test: Mast Move Down");
            fork.Translate(-Vector3.up * speedTranslate * Time.deltaTime);
            if(mastMoveTrue)
            {
                mast.Translate(-Vector3.up * speedTranslate * Time.deltaTime);
            }
        }
		//'=' key raises fork & mast 
        //if(Input.GetKey(KeyCode.Equals))
		if(OVRInput.Get(OVRInput.Button.Four))
        {
           fork.Translate(Vector3.up * speedTranslate * Time.deltaTime);
            if(mastMoveTrue)
            {
                mast.Translate(Vector3.up * speedTranslate * Time.deltaTime);
            }
          
        }
		
		
		if(Input.GetKey(KeyCode.Minus))
        {
            fork.Translate(-Vector3.right * speedTranslate * Time.deltaTime);
        }
		
        if(Input.GetKey(KeyCode.Equals))
        {
           fork.Translate(Vector3.right * speedTranslate * Time.deltaTime);
        }
	
	
    }//end fixedUpdate()
}

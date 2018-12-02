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
	
    

    private bool mastMoveTrue = false; //Activate or deactivate the movement of the mast

    // Update is called once per (fixed) frame
    void FixedUpdate () {
		
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
		
		
		
		//if mast already at max height
        if (mast.transform.position.y <= minYmast.y)
        {
            mast.transform.position = new Vector3(mast.transform.position.x, minYmast.y, mast.transform.position.z);
        }
		//if mast not already at max height
        if (mast.transform.position.y >= maxYmast.y)
        {
            mast.transform.position = new Vector3(mast.transform.position.x, maxYmast.y, mast.transform.position.z);
        }
		
		
		
		//'-' key lowers fork & mast 
        if (Input.GetKey(KeyCode.Minus))
        {
            fork.Translate(-Vector3.up * speedTranslate * Time.deltaTime);
            if(mastMoveTrue)
            {
                mast.Translate(-Vector3.up * speedTranslate * Time.deltaTime);
            }
        }
		//'=' key raises fork & mast 
        if (Input.GetKey(KeyCode.Equals))
        {
           fork.Translate(Vector3.up * speedTranslate * Time.deltaTime);
            if(mastMoveTrue)
            {
                mast.Translate(Vector3.up * speedTranslate * Time.deltaTime);
            }
          
        }
		

    }
}

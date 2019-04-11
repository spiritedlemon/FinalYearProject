using UnityEngine;
using System.Collections;

public class MastControl : MonoBehaviour {

    public Transform fork; 
    public Transform mast;
    public float speedTranslate; //Speed at which the fork raises
	
	public Vector3 maxYmast; //The maximum height of the mast
    public Vector3 minYmast; //The minimum height of the mast
    public float mastRot = 0; //track mast rotation
    public float rotSpeed = 1.5f; //speed of mast rotation
	
    public Vector3 maxY; //The maximum height of the platform
    public Vector3 minY; //The minimum height of the platform
	
	//Min and max of moving forks sideways
	public Vector3 sideMax;
	public Vector3 sideMin;

    public bool limitRight = false;
    public bool limitLeft = false;


    //Refs to other scripts
    public LeverControlOutput upDown;
    public LeverControlOutput leftRight;
    public LeverControlOutput tiltLever;


    private bool mastMoveTrue = false; //Whether or not you want to allow the mast movement
	
	

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

		
		//'-' key lowers fork & mast 
        //if(Input.GetKey(KeyCode.Minus))
        if(upDown.leverAngleOutput <= 10)
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
        if (upDown.leverAngleOutput >= -10)
        {
           fork.Translate(Vector3.up * speedTranslate * Time.deltaTime);
            if(mastMoveTrue)
            {
                mast.Translate(Vector3.up * speedTranslate * Time.deltaTime);
            }
          
        }

        //Move left
        if (leftRight.leverAngleOutput <= 10 && limitLeft == false)
        {
            fork.Translate(-Vector3.right * speedTranslate * Time.deltaTime);

            limitRight = false; //if moving left can no longer be at right limit

            if (fork.transform.localPosition.x <= sideMin.x)
            {
                limitLeft = true;
            }
        }

        //Move Right
        if (leftRight.leverAngleOutput >= -10 && limitRight == false)
        {
            fork.Translate(Vector3.right * speedTranslate * Time.deltaTime);

            limitLeft = false; //if moving right can no longer be at left limit

            if(fork.transform.localPosition.x >= sideMax.x)
            {
                limitRight = true;
            }
        }


        if (tiltLever.leverAngleOutput >= 10) 
        {
            if (mastRot < 1)
            {
                mastRot += Time.deltaTime * rotSpeed;
                mast.transform.localEulerAngles = new Vector3(mastRot, 0, 0);
            }
        }

        if (tiltLever.leverAngleOutput <= -10) 
        {
            if (mastRot > -5)
            {
                mastRot -= Time.deltaTime * rotSpeed;
                mast.transform.localEulerAngles = new Vector3(mastRot, 0, 0);
            }
        }



    }//end fixedUpdate()
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    
    This script simply maps the rotation from the 'SteeringWheel' object
    on to the 'steering' object which contains the mesh the players see

*/

public class wheelRotation : MonoBehaviour
{
    public SteeringWheelOutPut steeringWheelOutPut;
    public GameObject wheel;

    private float oldRot = 1000; //old rotation
    private float newRot;       //new/current rotation
    private float rotdiff;      //how much it has rotated in the last frame


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        newRot = steeringWheelOutPut.outAngle;

        if(oldRot != 1000)
        {
            rotdiff = newRot - oldRot;

            oldRot = newRot;
        }
        else
        {
            oldRot = newRot;
        }

        wheel.transform.localEulerAngles = new Vector3(
        wheel.transform.localEulerAngles.x,
        wheel.transform.localEulerAngles.y + rotdiff,
        wheel.transform.localEulerAngles.z
        );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheelRotation : MonoBehaviour
{

    public SteeringWheelOutPut steeringWheelOutPut;
    public GameObject wheel;

    private float oldRot = 1000;
    private float newRot;
    private float rotdiff;


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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour {
    [HideInInspector]
    public GameObject SteeringWheel;
    SteeringWheelController WheelController;
    bool SteeringWheelStick;


    public GameObject LeverObjectFR;
    public GameObject LeverObjectRL;
    public GameObject LeverObjectLR;
    public GameObject LeverObjectTilt;

    LeverController LeverControl;
    bool LeverStick;

    public bool rtriggerpulled = false;
    public bool ltriggerpulled = false;


    [Header("Steam Controllers Inputs (auto)")]
    [HideInInspector]
    public SteamVR_TrackedController VRJoystickTracker;

    private SteamVR_TrackedObject trackedObject;


    // Use this for initialization
    void Start () {
        VRJoystickTracker = gameObject.GetComponent<SteamVR_TrackedController>();
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObject.index);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //If hand is touching the object SteeringWheelCore && the trigger is pulled in 
        if (other.name == "SteeringWheelCore" && VRJoystickTracker.triggerPressed && !SteeringWheelStick)
        {
            SteeringWheel = other.gameObject;
            SteeringWheelStick = true;
            WheelController = SteeringWheel.GetComponent<SteeringWheelController>();
        }
       
        else if (other.name == "Lever(Forward/Reverse)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) 
        {
            LeverObjectFR = other.gameObject;
            LeverStick = true;
            LeverControl = LeverObjectFR.GetComponent<LeverController>();
        }
        else if (other.name == "Lever(Raise/Lower)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) 
        {
            LeverObjectFR = other.gameObject;
            LeverStick = true;
            LeverControl = LeverObjectFR.GetComponent<LeverController>();
        }
        else if (other.name == "Lever(Left/Right)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) 
        {
            LeverObjectFR = other.gameObject;
            LeverStick = true;
            LeverControl = LeverObjectFR.GetComponent<LeverController>();
        }
        //If hand is touching the object called Lever(Tilt) && the trigger is pulled in 
        else if (other.name == "Lever(Tilt)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) 
        {
            LeverObjectFR = other.gameObject;
            LeverStick = true;
            LeverControl = LeverObjectFR.GetComponent<LeverController>();
        }

    }

    

    void UnstickEveryThing()
    {

        if (SteeringWheelStick)
        {
            WheelController.OnUnStick();
            SteeringWheelStick = false; // STEERING WHEEL UNSTICK
            WheelController.Hand = null;
            SteeringWheel = null;
            WheelController = null;
        }

        if (LeverStick) //used for all levers
        {
            LeverControl.OnUnStick();
            LeverStick = false; // STEERING WHEEL UNSTICK
            LeverControl.Hand = null;
            LeverObjectFR = null;
            LeverControl = null;
        }
    }

    

    // Update is called once per frame
    void FixedUpdate ()
    {

        //Should be getPressDown but has a minimum time between presses which cause issues
        if (Controller.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            if (trackedObject.tag == "rhand") //if right hand trigger is pulled
            {
                rtriggerpulled = true; //set to true (This is used in Forklift control to add acceleration to vehicle
                //Debug.Log("accelerating");
            }
            if (trackedObject.tag == "lhand")
            {
                ltriggerpulled = true;
            }
        }
        if (Controller.GetPressUp(Valve.VR.EVRButtonId.k_EButton_Grip))
        {
            if (trackedObject.tag == "rhand")
            {
                rtriggerpulled = false;
                //Debug.Log("not accelerating");
            }
            if (trackedObject.tag == "lhand")
            {
                ltriggerpulled = false;
            }
        }

        if (SteeringWheelStick) 
        {
            if (!WheelController.Hand)
            {
                WheelController.Hand = gameObject; // CHECK IF ALREADY HAND GRABBED
            }
            WheelController.OnStick(VRJoystickTracker);
        }

        if (LeverStick) // STEERING WHEEL CONTROLLER
        {
            if (!LeverControl.Hand)
            {
                LeverControl.Hand = gameObject; // CHECK IF ALREADY HAND GRABBED
            }
            LeverControl.OnStick(VRJoystickTracker);
        }

        if (!VRJoystickTracker.triggerPressed) // UNSTICK EVERYTHING
        {
            UnstickEveryThing();
        }

       


    }
    
}

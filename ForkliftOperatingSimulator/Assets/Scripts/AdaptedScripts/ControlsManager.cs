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
        if (other.name == "SteeringWheelCore" && VRJoystickTracker.triggerPressed && !SteeringWheelStick)
        {
            SteeringWheel = other.gameObject;
            SteeringWheelStick = true;
            WheelController = SteeringWheel.GetComponent<SteeringWheelController>();
        }
       
        else if (other.name == "Lever(Forward/Reverse)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) // STICK ACCELERATE LEVER
        {
            LeverObjectFR = other.gameObject;
            LeverStick = true;
            LeverControl = LeverObjectFR.GetComponent<LeverController>();
        }
        else if (other.name == "Lever(Raise/Lower)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) // STICK ACCELERATE TRIGGER
        {
            LeverObjectFR = other.gameObject;
            LeverStick = true;
            LeverControl = LeverObjectFR.GetComponent<LeverController>();
        }
        else if (other.name == "Lever(Left/Right)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) // STICK ACCELERATE TRIGGER
        {
            LeverObjectFR = other.gameObject;
            LeverStick = true;
            LeverControl = LeverObjectFR.GetComponent<LeverController>();
        }
        else if (other.name == "Lever(Tilt)" && VRJoystickTracker.triggerPressed && !SteeringWheelStick) // STICK ACCELERATE TRIGGER
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

        if (LeverStick)
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

        if (Controller.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip)) // It is supposed to return a boolean
        {
            Debug.Log("heyy");
        }

        if (SteeringWheelStick) // STEERING WHEEL CONTROLLER
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

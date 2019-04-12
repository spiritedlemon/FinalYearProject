using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{

    [Header("Hand to track")]
    public GameObject Hand;
    public bool HandSticked = false;
    SteamVR_Controller.Device TrackedController = null;

    private float angleStickyOffset; // ofset between wheel rotation and hand position on grab
    private float wheelLastSpeed; // wheel speed at moment of ungrab, then reduces graduelly due to INERTIA
    private float INERTIA = 0.5f; // 1-wheel never stops // 0 - wheel stops instantly
    public float MAX_ROTATION = 45; //maximal degree rotation of the wheel
    private float WHEEL_HAPTIC_FREQUENCY = 360 / 6; //every wheel whill click 12 times per wheel rotation

    [Header("Steering Wheel Relative Point")]
    public GameObject WheelBase;

    [Header("Wheel & Hand relative position")]
    public Vector3 RelativePos;

    [Header("Output steering wheel angle")]
    public float outputAngle = 0;

    public LeverControlOutput LeverControlOutput;

    [Header("Arrays Values (Debug)")]
    public List<float> lastValues = new List<float>(); // stores last angles
    public List<float> Diffs = new List<float>(); // stores difference between each last angles
    public List<float> formulaDiffs = new List<float>(); // stores formulated diffs
    public List<float> increment = new List<float>(); // calculating incrementation

    void Start()
    {
        CreateArrays(5); // CALLING FUNCTION WHICH CREATES ARRAYS
        angleStickyOffset = 0f;
        HandSticked = false;
        wheelLastSpeed = 0;
    }

    public void OnStick(SteamVR_TrackedController TrackedController)
    {
        if (HandSticked != true)
        {
            CalculateOffset();
        }
        HandSticked = true;
        this.TrackedController = SteamVR_Controller.Input(checked((int)TrackedController.controllerIndex));


    }


    void CalculateOffset()
    {
        float rawAngle = CalculateRawAngle();
        angleStickyOffset = outputAngle - rawAngle;
    }

    public void OnUnStick()
    {
        HandSticked = false;
        wheelLastSpeed = outputAngle - lastValues[3];

        TrackedController = null;
    }

    float CalculateRawAngle()
    {
        RelativePos = WheelBase.transform.InverseTransformPoint(Hand.transform.position); // Relative Position betwen hand and lever

        return Mathf.Atan2(RelativePos.z, RelativePos.y) * Mathf.Rad2Deg; // Circular Data from X & Y vectors
    }


    void FixedUpdate()
    {

        LeverControlOutput.leverAngleOutput = outputAngle; //first action every frame is to output angle to Output script
        float angle;
        if (HandSticked)
        {
            angle = CalculateRawAngle() + angleStickyOffset; // When hands are holding the lever, their hand dictates how the wheel moves
            // angleSticky Offset is calculated when the lever is grabbed, this makes it not rotate to the user's hand
        }
        else
        {
            // when lever is released a small amount of inertia is applied to make it feel more real
            angle = outputAngle + wheelLastSpeed; //last rotation speed is updated when lever is 'ungrabbed' and then gradually returns to zero
            wheelLastSpeed *= INERTIA;
        }
        lastValues.RemoveAt(0); // Remove first item (Cycling through values)
        lastValues.Add(angle); // Add last item to array

        outputAngle = hookedAngles(angle);// Set output with function
       
        transform.localEulerAngles = new Vector3(outputAngle, 0, 0);

        float haptic_speed_coeff = Mathf.Abs(lastValues[4] - lastValues[3]) + 1;
        if (Mathf.Abs(outputAngle % WHEEL_HAPTIC_FREQUENCY) <= haptic_speed_coeff &&
            Mathf.Abs(lastValues[3] % WHEEL_HAPTIC_FREQUENCY) > haptic_speed_coeff)
        {
            if (TrackedController != null)
            {
                TrackedController.TriggerHapticPulse(1000);
            }
        }

    }


    void CreateArrays(int firstPparam) // FUNCTION WHICH CREATING ARRAYS
    {
        for (int i = 0; i < firstPparam; i++) // FOR LOOP WITH PARAM
        {
            lastValues.Add(0);  // LAST VALUES ARRAY
        }


        for (int i = 0; i < firstPparam - 1; i++) // FOR LOOP WITH PARAM -1
        {
            Diffs.Add(0); // ARRAY TO STORE DIFFERECE BETWEEN NEXT AND PREV
            formulaDiffs.Add(0); // ARRAY TO STORE FORMULATED DIFFS
            increment.Add(0); //  ARRAY TO STORE INCREMENT FOR EACH WHEEL SPIN

        }
    }


    public float hookedAngles(float angle) // FORMULATING AND CALCULATING FUNCTION WHICH COUNTS SPINS OF WHEEL
                                           //Also applying rotation limits
    {
        float period = 360;
        for (int i = 0; i < lastValues.Count - 1; i++)
        {
            Diffs.RemoveAt(0);
            Diffs.Add(lastValues[i + 1] - lastValues[i]);
        }

        for (int i = 0; i < formulaDiffs.Count; i++)
        {
            formulaDiffs.RemoveAt(0);
            var a = (Diffs[i] + period / 2.0f);
            var b = period;
            var fdiff = a - Mathf.Floor(a / b) * b;
            formulaDiffs.Add(fdiff - period / 2);
        }

        for (int i = 0; i < formulaDiffs.Count; i++)
        {
            increment.RemoveAt(0);
            increment.Add(formulaDiffs[i] - Diffs[i]);
        }

        for (int i = 1; i < formulaDiffs.Count; i++)
        {
            increment[i] += increment[i - 1];
        }

        lastValues[4] += increment[3];

        if (Mathf.Abs(lastValues[4]) > MAX_ROTATION)
        {
            lastValues[4] = lastValues[3];
            if (TrackedController != null)
            {
                TrackedController.TriggerHapticPulse(500);

            }
        }


        return lastValues[4]; // COLLIBRATE TO ZERO WHEN STILL AND RETURN CALCULATED VALUE
    }

}

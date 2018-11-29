using System;
using UnityEngine;

    [RequireComponent(typeof (ForkliftController))]
public class UserInputReader : MonoBehaviour
    {
        private ForkliftController FLCont; // the car controller we want to use

        private void Awake()
        {
            // Link to the controller
            FLCont = GetComponent<ForkliftController>();
        }


        private void FixedUpdate()
        {
            //Get user input and pass to controller
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");

            float handbrake = Input.GetAxis("Jump");
            FLCont.Move(x, y, y, handbrake);

            FLCont.Move(x, y, y, 0f);
        }
    }


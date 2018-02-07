using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Controller;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private SteeringWheel steeringWheel;

        bool vr = true;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            steeringWheel = GameObject.FindObjectOfType<SteeringWheel>();
        }
        


        private void FixedUpdate()
        {
            if(vr)
            {
                m_Car.Move(steeringWheel.Rotation, WandController.Accel, WandController.Brake, 0);
                return;
            }
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}

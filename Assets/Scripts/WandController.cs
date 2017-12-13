using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Controller
{
	public class WandController : MonoBehaviour
	{
		private SteamVR_TrackedController _c;
		
		void Start ()
		{
			_c = GetComponent<SteamVR_TrackedController>();
		}

		void Update ()
		{
			var states = new VRControllerAxis_t[]
			{
				_c.controllerState.rAxis0,
				_c.controllerState.rAxis1,
				_c.controllerState.rAxis2,
				_c.controllerState.rAxis3,
				_c.controllerState.rAxis4
			};

			for (int i = 0; i < states.Length; i++)
			{
				var state = states[i];
				
				Debug.Log(string.Format("Axis {0} - x: {0} y: {1}", i, state.x, state.y));
			}
		}
	}
}
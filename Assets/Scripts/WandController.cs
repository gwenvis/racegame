using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Controller
{
	/// Author: Antonio Bottelier
	/// You will die instantly if you copy this. 

	public class WandController : MonoBehaviour
	{
		public bool isLeftHand;
		private SteamVR_TrackedController _c;
		private Grabbable _currentlyGrabbing;
		private GameObject _model;
		
		void Start ()
		{
			_c = GetComponent<SteamVR_TrackedController>();
			_model = transform.Find("Model").gameObject;
		}

		void Update ()
		{
			float trigaxis = _c.controllerState.rAxis1.y;

			if (trigaxis < 0.1 && _currentlyGrabbing != null)
			{
				_currentlyGrabbing.StopGrab(this);
				_currentlyGrabbing = null;
			}
			else if (trigaxis > 0.1 && _currentlyGrabbing == null)
				Grab();
		}

		void Grab()
		{
			// detect what it's grabbing.
			var candidates = Physics.SphereCastAll(transform.position, 2, transform.forward, Mathf.Infinity);

			foreach (var candidate in candidates)
			{
				Grabbable g = candidate.collider.gameObject.GetComponent<Grabbable>();

				if (!g) continue;
				
				g.StartGrab(this);
				_currentlyGrabbing = g;
				break;
			}
		}

		public void HideControllerModel(bool hide = true)
		{
			_model.SetActive(!hide);
		}
	}
}
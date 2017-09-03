/**
 * Copyright (c) 2017 The Campfire Union Inc - All Rights Reserved.
 *
 * Licensed under the MIT license. See LICENSE file in the project root for
 * full license information.
 *
 * Email:   info@campfireunion.com
 * Website: https://www.campfireunion.com
 */

using UnityEngine;
using System.Collections;
using Valve.VR;

namespace VRKeys {

	public class OpenVRController : Controller {

		private SteamVR_Controller.Device _controller;
		private SteamVR_Controller.Device controller {
			get {
				if (_controller == null) {
					var trackedObject = GetComponentInParent<SteamVR_TrackedObject> ();
					if (trackedObject == null) return null;

					var deviceIndex = (int) trackedObject.index;
					if (deviceIndex == -1) return null;

					_controller = SteamVR_Controller.Input (deviceIndex);
				}
				return _controller;
			}
		}

		public override void TriggerPulse () {
			if (controller == null) return;

			controller.TriggerHapticPulse (800);
		}

		public override bool OnGrip () {
			if (controller == null) return false;

			return controller.GetPress (SteamVR_Controller.ButtonMask.Grip);
		}
	}
}
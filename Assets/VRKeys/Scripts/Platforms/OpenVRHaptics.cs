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

	public class OpenVRHaptics : Haptics {

		private int deviceIndex = -1;

		private SteamVR_Controller.Device controller;

		public override void TriggerPulse () {
			if (deviceIndex == -1) {
				var trackedObject = GetComponentInParent<SteamVR_TrackedObject> ();
				if (trackedObject == null) return;

				deviceIndex = (int) trackedObject.index;
				if (deviceIndex == -1) return;

				controller = SteamVR_Controller.Input (deviceIndex);
			}

			controller.TriggerHapticPulse (800);
		}
	}
}
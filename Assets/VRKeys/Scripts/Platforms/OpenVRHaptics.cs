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

		public override void TriggerPulse () {
			int deviceIndex = (mallet.hand == Mallet.MalletHand.Left)
				? SteamVR_Controller.GetDeviceIndex (SteamVR_Controller.DeviceRelation.FarthestLeft)
				: SteamVR_Controller.GetDeviceIndex (SteamVR_Controller.DeviceRelation.FarthestRight);

			if (deviceIndex == -1) return;

			var controller = SteamVR_Controller.Input (deviceIndex);

			controller.TriggerHapticPulse (800, EVRButtonId.k_EButton_Grip);
		}
	}
}
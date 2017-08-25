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

namespace VRKeys {

	public class OculusHaptics : Haptics {

		public override void TriggerPulse () {
			StartCoroutine (DoTriggerPulse ());
		}

		private IEnumerator DoTriggerPulse () {
			var controller = (mallet.hand == Mallet.MalletHand.Left) ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
			OVRInput.SetControllerVibration (0.3f, 0.3f, controller);
			yield return null;
			OVRInput.SetControllerVibration (0f, 0f, controller);
		}

		private void OnDisable () {
			var controller = (mallet.hand == Mallet.MalletHand.Left) ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
			OVRInput.SetControllerVibration (0f, 0f, controller);
		}
	}
}
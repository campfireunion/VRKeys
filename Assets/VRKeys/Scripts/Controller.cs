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
using UnityEngine.XR;
using System.Collections.Generic;

namespace VRKeys {

	/// <summary>
	/// Base class for platform-specific inputs and controller access.
	/// </summary>
	public class Controller : MonoBehaviour {
		protected Mallet mallet;

		private InputDevice _device;

		private InputDevice device {
			get {
				if (_device == null) {
					var devices = new List<InputDevice> ();
					InputDevices.GetDevicesWithRole (
						(mallet.hand == Mallet.MalletHand.Left) ? InputDeviceRole.LeftHanded : InputDeviceRole.RightHanded,
						devices
					);

					if (devices.Count > 0) {
						_device = devices[0];
					}
				}
				return _device;
			}
		}

		private void Start () {
			mallet = GetComponent<Mallet> ();
		}

		public void TriggerPulse () {
			if (device == null) return;

			device.SendHapticImpulse (0, 0.3f, 0.1f);
		}

		public bool OnGrip () {
			if (device == null) return false;

			return false;// device.GetPress (Grip);
		}
	}
}
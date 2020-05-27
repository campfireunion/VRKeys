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

	/// <summary>
	/// Backspace key.
	/// </summary>
	public class BackspaceKey : Key {

		public override void HandleTriggerEnter (Collider other) {
			PressKey ();
		}

		public override void PressKey () {
			keyboard.Backspace ();

			base.PressKey ();
		}

		public override void UpdateLayout (Layout translation) {
			label.text = translation.backspaceButtonLabel;
		}
	}
}
/**
 * Copyright (c) 2017 The Campfire Union Inc - All Rights Reserved.
 *
 * Unauthorized copying of this file, via any medium, is strictly prohibited.
 * This source code is proprietary and confidential.
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
			keyboard.Backspace ();

			ActivateFor (0.3f);
		}

		public override void UpdateLayout (Layout translation) {
			label.text = translation.backspaceButtonLabel;
		}
	}
}
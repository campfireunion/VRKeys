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
	/// Enter key that calls Submit() on the keyboard.
	/// </summary>
	public class EnterKey : Key {

		public override void HandleTriggerEnter (Collider other) {
			keyboard.Submit ();
		}

		public override void UpdateLanguage (Translation translation) {
			label.text = translation.enterButtonLabel;
		}
	}
}
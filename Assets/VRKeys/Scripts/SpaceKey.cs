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
	/// Space key.
	/// </summary>
	public class SpaceKey : Key {

		public override void HandleTriggerEnter (Collider other) {
			keyboard.AddCharacter (" ");

			ActivateFor (0.3f);
		}

		public override void UpdateLanguage (Translation translation) {
			label.text = translation.spaceButtonLabel;
		}
	}
}
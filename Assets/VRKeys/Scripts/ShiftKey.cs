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
	/// Shift key toggles between lower and upper case and symbols.
	/// </summary>
	public class ShiftKey : Key {

		bool shifted = false;

		public override void HandleTriggerEnter (Collider other) {
			shifted = keyboard.ToggleShift ();

			meshRenderer.material = shifted ? activeMat : inactiveMat;
		}

		public override void Enable () {
			base.Enable ();

			meshRenderer.material = shifted ? activeMat : inactiveMat;
		}
	}
}
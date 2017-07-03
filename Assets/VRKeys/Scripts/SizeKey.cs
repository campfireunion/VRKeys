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
	/// Key for resizing the keyboard.
	/// </summary>
	public class SizeKey : Key {

		public Keyboard.KeyboardSize size;

		private bool isActive = false;

		protected override void OnEnableExtras () {
			meshRenderer.material = isActive ? activeMat : inactiveMat;
		}

		public override void HandleTriggerEnter (Collider other) {
			keyboard.Resize (size);
		}

		public void SetActiveSize (Keyboard.KeyboardSize activeSize) {
			if (meshRenderer == null) {
				meshRenderer = GetComponent<MeshRenderer> ();
			}

			if (size == activeSize) {
				isActive = true;
				meshRenderer.material = activeMat;
			} else {
				isActive = false;
				meshRenderer.material = inactiveMat;
			}
		}
	}
}
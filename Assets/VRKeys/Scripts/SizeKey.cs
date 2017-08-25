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

		public override void UpdateLayout (Layout translation) {
			switch (size) {
				case Keyboard.KeyboardSize.Small:
					label.text = translation.smallButtonLabel;
					break;

				case Keyboard.KeyboardSize.Medium:
					label.text = translation.mediumButtonLabel;
					break;

				case Keyboard.KeyboardSize.Large:
					label.text = translation.largeButtonLabel;
					break;
			}
		}
	}
}
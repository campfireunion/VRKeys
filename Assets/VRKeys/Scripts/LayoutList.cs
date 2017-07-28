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
using VRKeys.Layouts;

namespace VRKeys {
	/// <summary>
	/// List of supported layouts.
	/// </summary>
	public enum KeyboardLayout {
		Qwerty,
		French,
		Dvorak
	}

	/// <summary>
	/// Fetches layouts.
	/// </summary>
	public static class LayoutList {

		/// <summary>
		/// Get a new keyboard layout object.
		/// </summary>
		/// <param name="layout">KeyboardLayout.</param>
		/// <returns>Layout object.</returns>
		public static Layout GetLayout (KeyboardLayout layout) {
			switch (layout) {
				case KeyboardLayout.French:
					return new French ();

				case KeyboardLayout.Dvorak:
					return new Dvorak ();

				default:
					return new Layout ();
			}
		}
	}
}
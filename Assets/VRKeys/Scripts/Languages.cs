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
using VRKeys.Translations;

namespace VRKeys {
	/// <summary>
	/// List of supported languages.
	/// </summary>
	public enum Language {
		English,
		French
	}

	/// <summary>
	/// Fetches translations.
	/// </summary>
	public static class TranslationList {

		/// <summary>
		/// Get a new translation object for the specified language.
		/// </summary>
		/// <param name="lang">Language.</param>
		/// <returns>Translation object.</returns>
		public static Translation GetTranslation (Language lang) {
			switch (lang) {
				case Language.French:
					return new French ();

				default:
					return new Translation ();
			}
		}
	}
}
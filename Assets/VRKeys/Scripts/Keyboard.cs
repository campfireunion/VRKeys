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
using UnityEngine.Events;
using System;
using System.Collections;
using TMPro;
using NewtonVR;

namespace VRKeys {
	/// <summary>
	/// Keyboard input system for use with NewtonVR. To use, drop the VRKeys prefab
	/// into your scene and activate as needed. Listen for OnUpdate and OnSubmit events,
	/// and set the text via SetText(string).
	/// 
	/// Input validation can be done during OnUpdate and OnSubmit events by calling
	/// ShowValidationMessage(msg) and HideValidationMessage(). The keyboard does not
	/// automatically hide OnSubmit, but rather you should call SetActive(false) when
	/// you have finished validating the submitted text.
	/// </summary>
	public class Keyboard : MonoBehaviour {

		public Vector3 positionRelativeToUser = new Vector3 (0f, 1.35f, 2f);

		[Space (10)]

		public TextMeshProUGUI placeholder;

		public string placeholderMessage = "Tap the keys to begin typing";

		public TextMeshProUGUI displayText;

		public GameObject validationNotice;

		public TextMeshProUGUI validationMessage;

		public GameObject infoNotice;

		public TextMeshProUGUI infoMessage;

		public GameObject successNotice;

		public TextMeshProUGUI successMessage;

		[Space (10)]

		public Color displayTextColor = Color.black;

		public Color caretColor = Color.gray;

		[Space (10)]

		public GameObject keyPrefab;

		public Transform keysParent;

		public float keyWidth = 0.16f;

		public float keyHeight = 0.16f;

		[Space (10)]

		public string text = "";

		[Space (10)]

		public GameObject leftMallet;

		public GameObject rightMallet;

		private string[] row1 = { "`", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };

		private string[] row1Shift = { "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "_", "+" };

		private string[] row2 = { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "[", "]", "\\" };

		private string[] row2Shift = { "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "{", "}", "|" };

		private string[] row3 = { "a", "s", "d", "f", "g", "h", "j", "k", "l", ";", "'" };

		private string[] row3Shift = { "A", "S", "D", "F", "G", "H", "J", "K", "L", ":", "\"" };

		private string[] row4 = { "z", "x", "c", "v", "b", "n", "m", ",", ".", "?" };

		private string[] row4Shift = { "Z", "X", "C", "V", "B", "N", "M", "<", ">", "/" };

		[Space (10)]

		public float row1Offset = 0.16f;

		public float row2Offset = 0.08f;

		public float row3Offset = 0f;

		public float row4Offset = -0.08f;

		[Space (10)]

		public bool leftPressing = false;

		public bool rightPressing = false;

		[Serializable]
		public class KeyboardUpdateEvent : UnityEvent<string> { }

		[Serializable]
		public class KeyboardSubmitEvent : UnityEvent<string> { }

		[Space (10)]

		/// <summary>
		/// Listen for events whenever the text changes.
		/// </summary>
		public KeyboardUpdateEvent OnUpdate = new KeyboardUpdateEvent ();

		/// <summary>
		/// Listen for events when Submit() is called.
		/// </summary>
		public KeyboardSubmitEvent OnSubmit = new KeyboardSubmitEvent ();

		private LetterKey[] keys;

		private bool shifted = false;

		// Use this for initialization
		void Start () {
			StartCoroutine (SetupKeys ());
		}

		private void OnEnable () {
			validationNotice.SetActive (false);
			infoNotice.SetActive (false);
			successNotice.SetActive (false);

			UpdateDisplayText ();
			PlaceholderVisibility ();

			StartCoroutine (PositionAndAttachMallets ());
		}

		IEnumerator PositionAndAttachMallets () {
			yield return new WaitUntil (() => NVRPlayer.Instances.Count > 0);

			transform.SetParent (NVRPlayer.Instance.gameObject.transform, false);
			transform.localPosition = positionRelativeToUser;
			
			leftMallet.transform.SetParent (NVRPlayer.Instance.LeftHand.transform);
			leftMallet.transform.localPosition = Vector3.zero;
			leftMallet.transform.localRotation = Quaternion.Euler (90f, 0f, 0f);
			leftMallet.SetActive (true);

			rightMallet.transform.SetParent (NVRPlayer.Instance.RightHand.transform);
			rightMallet.transform.localPosition = Vector3.zero;
			rightMallet.transform.localRotation = Quaternion.Euler (90f, 0f, 0f);
			rightMallet.SetActive (true);
		}

		private void OnDisable () {
			if (leftMallet != null) {
				leftMallet.SetActive (false);
			}

			if (rightMallet != null) {
				rightMallet.SetActive (false);
			}
		}

		/// <summary>
		/// Set the text value all at once.
		/// </summary>
		/// <param name="txt">New text value.</param>
		public void SetText (string txt) {
			text = txt;

			UpdateDisplayText ();
			PlaceholderVisibility ();

			OnUpdate.Invoke (text);
		}

		/// <summary>
		/// Add a character to the input text.
		/// </summary>
		/// <param name="character">Character.</param>
		public void AddCharacter (string character) {
			text += character;

			UpdateDisplayText ();
			PlaceholderVisibility ();

			OnUpdate.Invoke (text);
		}

		/// <summary>
		/// Toggle whether the characters are shifted (caps).
		/// </summary>
		public bool ToggleShift () {
			if (keys == null) {
				return false;
			}

			shifted = !shifted;

			foreach (LetterKey key in keys) {
				key.shifted = shifted;
			}

			return shifted;
		}

		/// <summary>
		/// Backspace one character.
		/// </summary>
		public void Backspace () {
			if (text.Length > 0) {
				text = text.Substring (0, text.Length - 1);
			}

			UpdateDisplayText ();
			PlaceholderVisibility ();

			OnUpdate.Invoke (text);
		}

		/// <summary>
		/// Submit and close the keyboard.
		/// </summary>
		public void Submit () {
			OnSubmit.Invoke (text);
		}

		/// <summary>
		/// Show the specified validation notice.
		/// </summary>
		/// <param name="message">Message to show.</param>
		public void ShowValidationMessage (string message) {
			validationMessage.text = message;
			validationNotice.SetActive (true);
			infoNotice.SetActive (false);
			successNotice.SetActive (false);
		}

		/// <summary>
		/// Show the specified input notice.
		/// </summary>
		/// <param name="message">Message to show.</param>
		public void ShowInfoMessage (string message) {
			infoMessage.text = message;
			validationNotice.SetActive (false);
			infoNotice.SetActive (true);
			successNotice.SetActive (false);
		}

		/// <summary>
		/// Show the specified success notice.
		/// </summary>
		/// <param name="message">Message to show.</param>
		public void ShowSuccessMessage (string message) {
			successMessage.text = message;
			validationNotice.SetActive (false);
			infoNotice.SetActive (false);
			successNotice.SetActive (true);
		}

		/// <summary>
		/// Hide the validation notice.
		/// </summary>
		public void HideValidationMessage () {
			validationNotice.SetActive (false);
		}

		/// <summary>
		/// Hide the info notice.
		/// </summary>
		public void HideInfoMessage () {
			infoNotice.SetActive (false);
		}

		/// <summary>
		/// Hide the success notice.
		/// </summary>
		public void HideSuccessMessage () {
			successNotice.SetActive (false);
		}

		/// <summary>
		/// Setup the keys.
		/// </summary>
		IEnumerator SetupKeys () {
			keys = new LetterKey[row1.Length + row2.Length + row3.Length + row4.Length];
			int keyCount = 0;

			// Numbers row
			for (int i = 0; i < row1.Length; i++) {
				GameObject obj = (GameObject) Instantiate (keyPrefab, keysParent);
				obj.transform.localPosition += (Vector3.right * ((keyWidth * i) - row1Offset));

				LetterKey key = obj.GetComponent<LetterKey> ();
				key.character = row1[i];
				key.shiftedChar = row1Shift[i];
				key.shifted = false;
				key.Init (obj.transform.localPosition);

				obj.name = "Key: " + row1[i];
				obj.SetActive (true);

				keys[keyCount] = key;
				keyCount++;

				yield return null;
			}

			// QWERTY row
			for (int i = 0; i < row2.Length; i++) {
				GameObject obj = (GameObject) Instantiate (keyPrefab, keysParent);
				obj.transform.localPosition += (Vector3.right * ((keyWidth * i) - row2Offset));
				obj.transform.localPosition += (Vector3.back * keyHeight * 1);

				LetterKey key = obj.GetComponent<LetterKey> ();
				key.character = row2[i];
				key.shiftedChar = row2Shift[i];
				key.shifted = false;
				key.Init (obj.transform.localPosition);

				obj.name = "Key: " + row2[i];
				obj.SetActive (true);

				keys[keyCount] = key;
				keyCount++;

				yield return null;
			}

			// ASDF row
			for (int i = 0; i < row3.Length; i++) {
				GameObject obj = (GameObject) Instantiate (keyPrefab, keysParent);
				obj.transform.localPosition += (Vector3.right * ((keyWidth * i) - row3Offset));
				obj.transform.localPosition += (Vector3.back * keyHeight * 2);

				LetterKey key = obj.GetComponent<LetterKey> ();
				key.character = row3[i];
				key.shiftedChar = row3Shift[i];
				key.shifted = false;
				key.Init (obj.transform.localPosition);

				obj.name = "Key: " + row3[i];
				obj.SetActive (true);

				keys[keyCount] = key;
				keyCount++;

				yield return null;
			}

			// ZXCV row
			for (int i = 0; i < row4.Length; i++) {
				GameObject obj = (GameObject) Instantiate (keyPrefab, keysParent);
				obj.transform.localPosition += (Vector3.right * ((keyWidth * i) - row4Offset));
				obj.transform.localPosition += (Vector3.back * keyHeight * 3);

				LetterKey key = obj.GetComponent<LetterKey> ();
				key.character = row4[i];
				key.shiftedChar = row4Shift[i];
				key.shifted = false;
				key.Init (obj.transform.localPosition);

				obj.name = "Key: " + row4[i];
				obj.SetActive (true);

				keys[keyCount] = key;
				keyCount++;

				yield return null;
			}
		}

		/// <summary>
		/// Update the display text, including trailing caret.
		/// </summary>
		private void UpdateDisplayText () {
			string display = (text.Length > 37) ? text.Substring (text.Length - 37) : text;

			displayText.text = string.Format (
				"<#{0}>{1}</color><#{2}>_</color>",
				ColorUtility.ToHtmlStringRGB (displayTextColor),
				display,
				ColorUtility.ToHtmlStringRGB (caretColor)
			);
		}

		/// <summary>
		/// Show/hide placeholder text.
		/// </summary>
		private void PlaceholderVisibility () {
			if (text == "") {
				placeholder.text = placeholderMessage;
				placeholder.gameObject.SetActive (true);
			} else {
				placeholder.gameObject.SetActive (false);
			}
		}
	}
}
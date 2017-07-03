using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace VRKeys {
	/// <summary>
	/// Example use of VRKeys keyboard.
	/// </summary>
	public class DemoScene : MonoBehaviour {

		/// <summary>
		/// Reference to the VRKeys keyboard.
		/// </summary>
		public Keyboard keyboard;

		/// <summary>
		/// See the following for why this is so convoluted:
		/// http://referencesource.microsoft.com/#System.ComponentModel.DataAnnotations/DataAnnotations/EmailAddressAttribute.cs,54
		/// http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx/
		/// </summary>
		private Regex emailValidator = new Regex (@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", RegexOptions.IgnoreCase);

		/// <summary>
		/// Show the keyboard with a custom input message.
		/// </summary>
		private void OnEnable () {
			keyboard.placeholderMessage = "Please enter your email address";
			keyboard.gameObject.SetActive (true);
		}

		/// <summary>
		/// Hide the validation message on update. Connect this to OnUpdate.
		/// </summary>
		public void HandleUpdate (string text) {
			keyboard.HideValidationMessage ();
		}

		/// <summary>
		/// Validate the email and simulate a form submission. Connect this to OnSubmit.
		/// </summary>
		public void HandleSubmit (string text) {
			keyboard.Disable ();

			if (! ValidateEmail (text)) {
				keyboard.ShowValidationMessage ("Please enter a valid email address");
				keyboard.Enable ();
				return;
			}


			StartCoroutine (SubmitEmail (text));
		}

		private IEnumerator SubmitEmail (string email) {
			keyboard.ShowInfoMessage ("Sending lots of spam, please wait... ;)");

			yield return new WaitForSeconds (2f);

			keyboard.ShowSuccessMessage ("Lots of spam sent to " + email);

			yield return new WaitForSeconds (2f);

			keyboard.HideSuccessMessage ();
			keyboard.SetText ("");
			keyboard.Enable ();
		}

		private bool ValidateEmail (string text) {
			if (! emailValidator.IsMatch (text)) {
				return false;
			}
			return true;
		}
	}
}
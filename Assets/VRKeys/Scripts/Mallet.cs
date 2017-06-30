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
using NewtonVR;

namespace VRKeys {
	/// <summary>
	/// Attaches to the end of the mallet that collides with the keys.
	/// </summary>
	public class Mallet : MonoBehaviour {

		public AudioClip clipToPlay;

		public enum MalletHand {
			Left,
			Right
		}

		public MalletHand hand;

		private AudioSource audioSource;

		private void Awake () {
			audioSource = GetComponent<AudioSource> ();
		}

		/// <summary>
		/// Called by the key that hit it if the collision was successful.
		/// </summary>
		/// <param name="key">Key.</param>
		public void HandleTriggerEnter (Key key) {
			audioSource.PlayOneShot (clipToPlay);

			if (transform.parent.transform.parent != null) {
				NVRHand hand = transform.parent.transform.parent.gameObject.GetComponent<NVRHand> ();
				if (hand != null) {
					hand.TriggerHapticPulse (800, NVRButtons.Grip);
				}
			}
		}
	}
}
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
using UnityEngine.VR;
using System.Collections;
using NewtonVR;
using Valve.VR;

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

		public bool isMovingDownward {
			get { return _isMovingDownward; }
			private set { _isMovingDownward = value; }
		}

		private bool _isMovingDownward = false;

		private AudioSource audioSource;

		private Haptics haptics;

		private Vector3 prevPos = Vector3.zero;

		private void Awake () {
			audioSource = GetComponent<AudioSource> ();

			switch (VRSettings.loadedDeviceName) {
				case "Oculus":
					haptics = gameObject.AddComponent<OculusHaptics> ();
					break;

				case "OpenVR":
					haptics = gameObject.AddComponent<OpenVRHaptics> ();
					break;
			}
		}

		private void FixedUpdate () {
			Vector3 curVel = (transform.position - prevPos) / Time.fixedDeltaTime;

			isMovingDownward = (curVel.y <= 0f);

			prevPos = transform.position;
		}

		/// <summary>
		/// Called by the key that hit it if the collision was successful.
		/// </summary>
		/// <param name="key">Key.</param>
		public void HandleTriggerEnter (Key key) {
			audioSource.PlayOneShot (clipToPlay);

			if (transform.parent.transform.parent != null) {
				haptics.TriggerPulse ();
			}
		}
	}
}
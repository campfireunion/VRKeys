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
using TMPro;

namespace VRKeys {
	/// <summary>
	/// An individual key in the VR keyboard.
	/// </summary>
	public class Key : MonoBehaviour {

		public Keyboard keyboard;

		public TextMeshPro label;

		public Material inactiveMat;

		public Material activeMat;

		public Vector3 defaultPosition;

		public Vector3 pressedPosition;

		public Vector3 pressDirection = Vector3.down;

		public float pressMagnitude = 0.1f;

		public bool autoInit = false;

		private bool isPressing = false;

		protected MeshRenderer meshRenderer;

		private IEnumerator _ActivateFor;

		private IEnumerator _Press;

		private void Awake () {
			meshRenderer = GetComponent<MeshRenderer> ();

			if (autoInit) {
				Init (transform.localPosition);
			}
		}

		/// <summary>
		/// Initialize the key with a default position and pressed position.
		/// </summary>
		/// <param name="defaultPos">Default position.</param>
		public void Init (Vector3 defaultPos) {
			defaultPosition = defaultPos;
			pressedPosition = defaultPos + (Vector3.down * 0.01f);
		}

		private void OnEnable () {
			isPressing = false;
			transform.localPosition = defaultPosition;
			meshRenderer.material = inactiveMat;
		}

		public void OnTriggerEnter (Collider other) {
			if (isPressing) {
				return;
			}

			Mallet mallet = other.gameObject.GetComponent<Mallet> ();
			if (mallet != null) {
				if (mallet.hand == Mallet.MalletHand.Left && keyboard.leftPressing) {
					return;
				} else if (mallet.hand == Mallet.MalletHand.Right && keyboard.rightPressing) {
					return;
				}

				if (_Press != null && _Press.MoveNext ()) {
					StopCoroutine (_Press);
				}
				_Press = Press (other, mallet);
				StartCoroutine (_Press);
			}
		}

		private IEnumerator Press (Collider other, Mallet mallet) {
			isPressing = true;

			if (mallet.hand == Mallet.MalletHand.Left) {
				keyboard.leftPressing = true;
			} else if (mallet.hand == Mallet.MalletHand.Right) {
				keyboard.rightPressing = true;
			}

			mallet.HandleTriggerEnter (this);
			HandleTriggerEnter (other);

			transform.localPosition = pressedPosition;

			yield return new WaitForSeconds (0.15f);

			transform.localPosition = defaultPosition;
			isPressing = false;

			if (mallet.hand == Mallet.MalletHand.Left) {
				keyboard.leftPressing = false;
			} else if (mallet.hand == Mallet.MalletHand.Right) {
				keyboard.rightPressing = false;
			}
		}

		/// <summary>
		/// Override this to handle trigger events. Only fires when
		/// a downward trigger event occurred from the collider
		/// matching keyboard.colliderName.
		/// </summary>
		/// <param name="other">Collider.</param>
		public virtual void HandleTriggerEnter (Collider other) {
			// Override me!
		}

		/// <summary>
		/// Show the active material for the specified length of time.
		/// </summary>
		/// <param name="seconds">Seconds.</param>
		public void ActivateFor (float seconds) {
			if (_ActivateFor != null && _ActivateFor.MoveNext ()) {
				StopCoroutine (_ActivateFor);
			}
			_ActivateFor = DoActivateFor (seconds);
			StartCoroutine (_ActivateFor);
		}

		private IEnumerator DoActivateFor (float seconds) {
			meshRenderer.material = activeMat;

			yield return new WaitForSeconds (seconds);

			meshRenderer.material = inactiveMat;
		}
	}
}
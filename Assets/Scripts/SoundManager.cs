using UnityEngine;
using System.Collections;

/// <summary>
/// Sound manager.
/// This is for playing sounds like ambient sounds and music.
/// </summary>
public class SoundManager : MonoSingleton<SoundManager> {

	#region Serialized Variables
	/// <summary>
	/// The ambient audio clips
	/// This needs to be set from the editor
	/// </summary>
	[SerializeField]
	protected AudioClip[] ambient;

	/// <summary>
	/// The ambient audio source.
	/// This needs to be set from the editor
	/// </summary>
	[SerializeField]
	protected AudioSource ambientSource;

	/// <summary>
	/// The play ambient automatically flag
	/// </summary>
	public bool playAmbientAutomatically = true;
	#endregion

	/// <summary>
	/// Gets a value indicating whether the ambient audio is playing or not.
	/// </summary>
	/// <value><c>true</c> if the ambient audio is playing; otherwise, <c>false</c>.</value>
	public bool IsAmbientPlaying {
		get {
			if (ambientSource != null) {
				return ambientSource.isPlaying;
			} else {
				return false;
			}
		}
	}

	/// <summary>
	/// Handles the enable event.
	/// </summary>
	void OnEnable () {
		if (playAmbientAutomatically) {
			PlayAmbient ();
		}
	}

	/// <summary>
	/// Plays a random ambient audio clip
	/// </summary>
	public void PlayAmbient () {
		if (ambientSource == null) {
			throw new System.NullReferenceException ("Ambient Source must be set");
		}
		if (ambient.Length == 0) {
			throw new System.InvalidOperationException ("No ambient audio clips configured");
		}

		// skip if already playing.
		if (ambientSource.isPlaying) {
			return;
		}

		// Set to random audio clip
		ambientSource.clip = ambient[ Random.Range (0, ambient.Length) ];
		ambientSource.Play ();
	}

	/// <summary>
	/// Stops the ambient audio
	/// </summary>
	public void StopAmbient () {
		if (ambientSource != null) {
			ambientSource.Stop();
		}
	}
}

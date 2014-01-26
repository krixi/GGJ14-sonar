using UnityEngine;
using System.Collections;

/// <summary>
/// Sound manager.
/// This is for playing sounds like ambient sounds and music.
/// </summary>
public class SoundManager : MonoSingleton<SoundManager> {

	public AudioSource musicSource;

	public void PlayMusic () {
		if (musicSource == null) {
			throw new System.NullReferenceException ("Music Source must be set");
		}
		musicSource.Play ();
	}

	public void StopMusic () {
		if (musicSource != null) {
			musicSource.Stop();
		}
	}
}

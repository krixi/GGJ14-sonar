using UnityEngine;
using System.Collections;

/// <summary>
/// Sound manager.
/// This is for playing sounds like ambient sounds and music.
/// </summary>
public class SoundManager : MonoSingleton<SoundManager> {

	public AudioSource musicSource;

	public AudioClip musicClip;

	public void PlayMusic () {
		if (musicSource == null) {
			musicSource = GetComponent<AudioSource>();
		}

		if (musicSource.isPlaying) {
			return;
		}

		musicSource.clip = musicClip;
		musicSource.Play ();

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		if (player != null) {
			transform.parent = player.transform;
		}
	}

	public void StopMusic () {
		if (musicSource != null) {
			musicSource.Stop();
		}
	}
}

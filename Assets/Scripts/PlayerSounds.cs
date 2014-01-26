using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSounds : MonoBehaviour {

	public PlayerController player;
	public EchoSphere echos;

	public AudioClip[] flapUpSounds;
	public AudioClip[] flapDownSounds;
	public AudioClip[] squeakSounds;
	
	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GetComponent<PlayerController>();
		}
		player.OnWingsFlapStarted += HandleOnWingsFlapStarted;
		player.OnWingsFlapEnded += HandleOnWingsFlapEnded;

		if (echos != null) {
			echos.OnPulseStarted += HandleOnPulseStarted;
		} else {
			Debug.LogWarning ("Must assign the echo script to player sounds");
		}
	}

	void HandleOnPulseStarted ()
	{
		// play a squeak sound.
		if (squeakSounds.Length > 0) {
			audio.PlayOneShot (squeakSounds[ Random.Range(0, squeakSounds.Length) ]);
		} else {
			Debug.LogWarning ("No squeak sounds configured");
		}
	}

	void HandleOnWingsFlapEnded ()
	{
		if (flapUpSounds.Length > 0) {
			audio.PlayOneShot (flapUpSounds[ Random.Range(0, flapUpSounds.Length) ]);
		} else {
			Debug.LogWarning ("No flap up sounds configured");
		}
	}

	void HandleOnWingsFlapStarted ()
	{
		if (flapDownSounds.Length > 0) {
			audio.PlayOneShot (flapDownSounds[ Random.Range(0, flapDownSounds.Length) ]);
		} else {
			Debug.LogWarning ("No flap down sounds configured");
		}
	}
}

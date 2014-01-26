using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSounds : MonoBehaviour {

	public PlayerController player;

	public AudioClip[] flapUpSounds;
	public AudioClip[] flapDownSounds;
	
	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GetComponent<PlayerController>();
		}
		player.OnWingsFlapStarted += HandleOnWingsFlapStarted;
		player.OnWingsFlapEnded += HandleOnWingsFlapEnded;
	}

	void HandleOnWingsFlapEnded ()
	{
		if (flapUpSounds.Length > 0) {
			audio.PlayOneShot (flapUpSounds[ Random.Range(0, flapUpSounds.Length) ]);
		}
	}

	void HandleOnWingsFlapStarted ()
	{
		if (flapDownSounds.Length > 0) {
			audio.PlayOneShot (flapDownSounds[ Random.Range(0, flapDownSounds.Length) ]);
		}
	}
}

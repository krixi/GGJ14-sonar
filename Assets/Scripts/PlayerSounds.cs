using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSounds : MonoBehaviour {

	public PlayerController player;
	
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
		audio.Stop ();
	}

	void HandleOnWingsFlapStarted ()
	{
		audio.Play ();
	}
}

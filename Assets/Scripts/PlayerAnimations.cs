using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimations : MonoBehaviour {
	
	public PlayerController player;

	public Transform playerSprite;

	// Use this for initialization
	void Start () {
		if (player == null) {
			player = GetComponent<PlayerController>();
		}
		player.OnWingsFlapStarted += HandleWingsFlap;
		player.OnWingsFlapEnded += HandleWingsFlap;
	}

	void HandleWingsFlap ()
	{
		if (playerSprite != null) {
			playerSprite.transform.localScale = new Vector3 (playerSprite.transform.localScale.x, -playerSprite.transform.localScale.y, playerSprite.transform.localScale.z);
		}
	}
}

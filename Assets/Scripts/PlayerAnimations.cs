using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimations : MonoBehaviour {
	
	public PlayerController player;

	public SpriteRenderer spriteRenderer;
	public Sprite flapUpSprite;
	public Sprite flapDownSprite;

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
		if (spriteRenderer != null) {
			spriteRenderer.sprite = flapUpSprite;
		}
	}

	void HandleOnWingsFlapStarted ()
	{
		if (spriteRenderer != null) {
			spriteRenderer.sprite = flapDownSprite;
		}		
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
	
	private float currentMovement = 0f;

	public float speed = 2f;

	public float maxSpeed = 100f;

	public float jumpForce = 5f;

	private bool jumped = false;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rigidbody2D == null) {
			throw new System.InvalidOperationException ("No rigidbody");
		}

		// Check for wing flapping
		if (Input.GetAxis("Jump") != 0f) {
			if (!jumped) { 
				rigidbody2D.AddForce (new Vector2 (0f, jumpForce));
				jumped = true;
			}
		} else {
			jumped = false;
		}

		// Get horizontal movement
		currentMovement = Input.GetAxis ("Horizontal");

		// Set the velocity
		//rigidbody2D.velocity = new Vector2 (currentMovement, 0f) * speed;
		rigidbody2D.AddForce ( new Vector2 (currentMovement, 0f) * speed);

		if (rigidbody2D.velocity.x > 0) {
			rigidbody2D.velocity = new Vector2(Mathf.Min (maxSpeed, rigidbody2D.velocity.x), rigidbody2D.velocity.y);
		} else {
			rigidbody2D.velocity = new Vector2(Mathf.Max (-maxSpeed, rigidbody2D.velocity.x), rigidbody2D.velocity.y);
		}
	}
}

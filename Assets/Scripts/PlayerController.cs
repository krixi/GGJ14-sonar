using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
	

	private float currentMovement = 0f;

	public float speed = 2f;

	public float maxSpeed = 100f;

	public float jumpForce = 5f;

	private bool jumped = false;

	public event System.Action OnWingsFlapStarted;
	public event System.Action OnWingsFlapEnded;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rigidbody == null) {
			throw new System.InvalidOperationException ("No rigidbody");
		}

		// Check for wing flapping
		if (Input.GetAxis("Jump") != 0f) {
			if (!jumped) { 
				rigidbody.AddForce (new Vector3 (0f, jumpForce));
				jumped = true;
				// Send the event to listeners
				if (OnWingsFlapStarted != null) {
					OnWingsFlapStarted ();
				}
			}
		} else {
			if (jumped) {
				jumped = false;
				if (OnWingsFlapEnded != null) {
					OnWingsFlapEnded ();
				}
			}
		}

		// Get horizontal movement
		currentMovement = Input.GetAxis ("Horizontal");

		// Set the velocity
		//rigidbody.velocity = new Vector3 (currentMovement, 0f) * speed;
		rigidbody.AddForce ( new Vector3 (currentMovement, 0f) * speed);

		if (rigidbody.velocity.x > 0) {
			rigidbody.velocity = new Vector3(Mathf.Min (maxSpeed, rigidbody.velocity.x), rigidbody.velocity.y);
		} else {
			rigidbody.velocity = new Vector3(Mathf.Max (-maxSpeed, rigidbody.velocity.x), rigidbody.velocity.y);
		}
	}
}

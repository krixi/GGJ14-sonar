using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	/// <summary>
	/// The health.
	/// </summary>
	public float health = 100f;

	/// <summary>
	/// The health lost per collision.
	/// </summary>
	public float healthLostPerCollision = 20f;

	/// <summary>
	/// The max health.
	/// </summary>
	public float maxHealth = 100f;

	/// <summary>
	/// The health bar.
	/// </summary>
	public GUIProgressBar healthBar;

	/// <summary>
	/// The energy.
	/// </summary>
	public float energy = 100f;

	/// <summary>
	/// The max energy.
	/// </summary>
	public float maxEnergy = 100f;

	/// <summary>
	/// The energy restore rate.
	/// </summary>
	public float energyRestoreRate = 5f;

	/// <summary>
	/// The energy bar.
	/// </summary>
	public GUIProgressBar energyBar;

	// Update is called once per frame
	void Update () {
		//energy = Mathf.Min (maxEnergy, Mathf.Lerp (energy, maxEnergy, energyRestoreRate * Time.deltaTime));
		energy += energyRestoreRate * Time.deltaTime;
		if (energy > maxEnergy ) {
			energy = maxEnergy;
		}
		if (energyBar != null) {
			energyBar.barDisplay = (energy / maxEnergy);
		}
	}

	/// <summary>
	/// Sets the health.
	/// </summary>
	/// <param name="_h">_h.</param>
	public void SetHealth (float _h) {
		health = _h;
		if (healthBar != null) {
			healthBar.barDisplay = (health / maxHealth);
		}
	}

	void OnCollisionEnter(Collision collision) {
		/* Left this in incase we want to play sound on wall collision
		 * foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay(contact.point, contact.normal, Color.white);
		}
		if (collision.relativeVelocity.magnitude > 2)
			audio.Play();*/
		
		// Lose health when colliding with terrtain
		SetHealth(Mathf.Max (health - healthLostPerCollision, 0));
	}
}

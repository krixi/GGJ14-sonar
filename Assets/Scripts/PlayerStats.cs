﻿using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	/// <summary>
	/// The health.
	/// </summary>
	public float health = 100f;

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

	// Update is called once per frame
	void Update () {
		//energy = Mathf.Min (maxEnergy, Mathf.Lerp (energy, maxEnergy, energyRestoreRate * Time.deltaTime));
		energy += energyRestoreRate * Time.deltaTime;
		if (energy > maxEnergy ) {
			energy = maxEnergy;
		}
	}

	/// <summary>
	/// Sets the health.
	/// </summary>
	/// <param name="_h">_h.</param>
	public void SetHealth (float _h) {
		health = _h;
	}
}

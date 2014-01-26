using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour {

	/// <summary>
	/// Spider will patrol between his position + offsetA and position + offsetB
	/// </summary>
	public float posOffsetY = 10f;
	public float negOffsetY = -10f;
	public float posOffsetX = 0f;
	public float negOffsetX = 0f;
	public Vector3 offsetA;
	public Vector3 offsetB;
	public Vector3 posA;
	public Vector3 posB;
	public float patrolTimeRoundTrip = 10f; // seconds
	public bool movingTowardA;
	/// <summary>
	/// The last (transform.position - offsetA/offsetB).magnitude
	/// </summary>
	public float lastDistTillTarget;

	// Use this for initialization
	void Start () {
		offsetA = new Vector3(posOffsetX,posOffsetY,0);
		offsetB = new Vector3(negOffsetX,negOffsetY,0);
		posA = transform.position + offsetA;
		posB = transform.position + offsetB;
		movingTowardA = true;
		lastDistTillTarget = (transform.position - posA).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pathVector;
		if (movingTowardA) {
			pathVector = offsetA - offsetB;
		} else {
			pathVector = offsetB - offsetA;
		}
		float spiderSpeed = pathVector.magnitude / (patrolTimeRoundTrip / 2f);
		float displacement = spiderSpeed * Time.deltaTime;
		Vector3 displacementVector = pathVector.normalized * displacement;
		transform.position += displacementVector;
		// Are we at posA? Turn around
		if(movingTowardA) {
			float distToTarget = (transform.position - posA).magnitude;
			// Check if we passed it
			if (distToTarget > lastDistTillTarget) {
				movingTowardA = false;
				lastDistTillTarget = (transform.position - posB).magnitude;
			} else {
				lastDistTillTarget = distToTarget;
			}
		} else {
		// Are we at posB? Turn around
			float distToTarget = (transform.position - posB).magnitude;
			// Check if we passed it
			if (distToTarget > lastDistTillTarget) {
				movingTowardA = true;
				lastDistTillTarget = (transform.position - posA).magnitude;
			} else {
				lastDistTillTarget = distToTarget;
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class SpiderController : MonoBehaviour {

	/// <summary>
	/// Spider will patrol between his position + offsetA and position + offsetB
	/// </summary>
	public Vector3 offsetA;
	public Vector3 offsetB;
	private Vector3 posA;
	private Vector3 posB;
	public float patrolTimeRoundTrip = 10f; // seconds
	public bool movingTowardA;
	/// <summary>
	/// The last (transform.position - offsetA/offsetB).magnitude
	/// </summary>
	public float lastDistTillTarget;

	// Use this for initialization
	void Start () {
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

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position + offsetA, 1f);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position + offsetB, 1f);
	}
}

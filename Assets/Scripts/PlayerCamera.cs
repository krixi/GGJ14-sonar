using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public Vector3 myPos = new Vector3(0,0,-3);

	public float followSpeed = 5f;
	
	public Transform myPlay;

	// Update is called once per frame
	void LateUpdate () {
		transform.position = Vector3.Lerp(transform.position, myPlay.position + myPos, followSpeed * Time.deltaTime);
	}
}

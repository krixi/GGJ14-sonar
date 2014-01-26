using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public Vector3 myPos = new Vector3(0,0,-3);
	
	public Transform myPlay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = myPlay.position + myPos;
	}
}

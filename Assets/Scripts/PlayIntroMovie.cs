using UnityEngine;
using System.Collections;

public class PlayIntroMovie : MonoBehaviour {

	public MovieTexture movTexture;

	void Start() {
		renderer.material.mainTexture = movTexture;
		movTexture.Play();

		Invoke ("LoadFirstLevel", movTexture.duration);
	}

	public void LoadFirstLevel () {
		GameManager.instance.LoadFirstLevel ();
	}

	public void Update () {
		if (Input.GetMouseButton (0)) {
			movTexture.Stop ();
			LoadFirstLevel ();
		}
	}
}

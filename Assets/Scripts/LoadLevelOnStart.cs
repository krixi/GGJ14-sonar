using UnityEngine;
using System.Collections;

public class LoadLevelOnStart : MonoBehaviour {

	public string levelToLoad = "Level1";

	// Use this for initialization
	void Start () {
		Application.LoadLevel (levelToLoad);
	}
}

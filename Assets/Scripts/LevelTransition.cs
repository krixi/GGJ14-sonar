using UnityEngine;
using System.Collections;

public class LevelTransition : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		GameManager.instance.LoadNextLevel();
	}
}

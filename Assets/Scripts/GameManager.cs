using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : ResourceSingleton<GameManager> {
	public List<string> levels = new List<string>();

	private int currentLevel = 0;

	public override void Init ()
	{
		base.Init ();
		currentLevel = PlayerPrefs.GetInt ("PlayerLevel", 0);
	}

	public void LoadNextLevel () {
		if (currentLevel+1 < levels.Count) {
			currentLevel++;
			PlayerPrefs.SetInt ("PlayerLevel", currentLevel);
			PlayerPrefs.Save ();
			Application.LoadLevel (levels[currentLevel]);
		} else {
			Debug.LogError ("No more errors!");
		}
	}
}

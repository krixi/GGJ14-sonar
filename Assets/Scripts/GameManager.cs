using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Game manager.
/// </summary>
public class GameManager : MonoSingleton<GameManager> {

	/// <summary>
	/// The level name format.
	/// Each level must be named exactly this way
	/// </summary>
	public string levelNameFormat = "Level{0}";

	/// <summary>
	/// The total number of levels.
	/// </summary>
	public int totalLevels = 2;

	/// <summary>
	/// The finished game scene.
	/// </summary>
	public string finishedGameScene = "GameComplete";

	/// <summary>
	/// The current level.
	/// </summary>
	private int currentLevel = 1;

	public int CurrentLevel {
		get { return currentLevel; }
	}
	

	protected override void Init ()
	{
		base.Init ();
<<<<<<< HEAD
		currentLevel = PlayerPrefs.GetInt ("PlayerLevel", 0);
=======
>>>>>>> master
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void LoadNextLevel () {
		if (currentLevel+1 <= totalLevels) {
			currentLevel++;
			Application.LoadLevel (string.Format(levelNameFormat, currentLevel));
		} else {
			Application.LoadLevel (finishedGameScene);
		}
	}

	public void RestartCurrentLevel() {
		Application.LoadLevel (Application.loadedLevel);
	}
}

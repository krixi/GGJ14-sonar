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
	/// The name of the menu scene.
	/// TODO: need a main menu to load
	/// </summary>
	public string menuSceneName = "MainMenu";

	/// <summary>
	/// The finished game scene.
	/// TODO: need a game complete scene to load.
	/// </summary>
	public string finishedGameScene = "GameComplete";

	/// <summary>
	/// The current level.
	/// </summary>
	private int currentLevel = 0;
	

	protected override void Init ()
	{
		base.Init ();
		PlayerPrefs.DeleteKey("PlayerLevel");
		currentLevel = PlayerPrefs.GetInt ("PlayerLevel", 0);
	}

	/// <summary>
	/// Loads the next level.
	/// </summary>
	public void LoadNextLevel () {
		if (currentLevel+1 < totalLevels) {
			currentLevel++;
			PlayerPrefs.SetInt ("PlayerLevel", currentLevel);
			PlayerPrefs.Save ();
			Application.LoadLevel (string.Format(levelNameFormat, currentLevel));
		} else {
			Debug.LogError ("No more levels!");
		}
	}
}

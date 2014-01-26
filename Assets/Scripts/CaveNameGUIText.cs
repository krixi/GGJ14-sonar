using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class CaveNameGUIText : MonoBehaviour {

	public GUIText text;

	public string caveNameFormat = "Cave {0}";

	// Use this for initialization
	void Start () {
		if (text == null) {
			text = GetComponent<GUIText>();
		}

		text.text = string.Format (caveNameFormat, GameManager.instance.CurrentLevel);
	}
}

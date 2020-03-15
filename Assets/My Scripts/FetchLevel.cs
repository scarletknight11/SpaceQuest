using UnityEngine;
using System.Collections;

public class FetchLevel : MonoBehaviour {

	/// <summary>
	/// Get player progress.
	/// </summary>

	void Awake () {
		GetComponent<TextMesh>().text = ((int)PlayerPrefs.GetInt("playerProgress", 1)).ToString();
	}

}

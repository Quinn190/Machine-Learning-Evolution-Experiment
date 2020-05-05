using UnityEngine;
using System.Collections;

public class ModeEngine : MonoBehaviour {
	public string Mode;
	public string BreedMode;
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Mode = "Camera Mode";
		}
		else if (Input.GetKeyDown(KeyCode.T)) {
			Mode = "Tile Mode";
		}
		else if (Input.GetKeyDown(KeyCode.M)) {
			Mode = "Monster Mode";
		}
	}
}

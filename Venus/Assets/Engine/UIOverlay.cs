using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOverlay : MonoBehaviour {
	bool isactive = false;
	int totalturns = 0;
	int creaturesalive = 0;
	string display = "Test";
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.O)) {
			isactive = !isactive;
		}
	}

	void OnGUI() {
		if (isactive == true) {
			display = GUI.TextField(new Rect(10, 70, 200, 20), display, 25);
		}
	}

}

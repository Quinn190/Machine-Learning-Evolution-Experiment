using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {
	public ModeEngine ME;
	public Tile selectedTile;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (ME.Mode == "Tile Mode") {
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (ray, out hit, 1000)) {
					selectedTile = hit.collider.gameObject.GetComponent<Tile> ();
					
				}
			}
		}
	}
}

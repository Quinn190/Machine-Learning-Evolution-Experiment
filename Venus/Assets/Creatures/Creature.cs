using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {
	public Tile currenttile;
	public int x;
	public int y;
	public MapGenerator mg;
	
	

	void Start() {
		
	}

	public void Move (Tile tile) {
		currenttile = tile;
		transform.position = tile.transform.position;
	}

	public void MoveDir(int x, int y) {
		Move(mg.Tilelist[currenttile.xcoord + x, currenttile.ycoord + y]);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			MoveDir(-1, 0);
		}

		Debug.DrawRay(transform.position, Vector3.back * 10, Color.blue);
		Debug.DrawRay(transform.position, Vector3.right * 10, Color.green);
		Debug.DrawRay(transform.position, Vector3.left * 10, Color.red);
		Debug.DrawRay(transform.position, Vector3.forward * 10, Color.yellow);
		
	}



}

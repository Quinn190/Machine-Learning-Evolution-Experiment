using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : MonoBehaviour {
	public Tile spawnpoint1;
	public Tile spawnpoint2;
	public MapGenerator mg;

	public Creature c1;
	public Creature c2;
	// Use this for initialization
	void Start () {
		spawnpoint1 = mg.spawntile1;
		spawnpoint2 = mg.spawntile2;
		Spawncreature();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Spawncreature () {

		c1 = Instantiate (c1, spawnpoint1.transform.position, Quaternion.identity) as Creature;	
		c1.currenttile = spawnpoint1;
		c1.x = 0;
		c1.y = 0;
		
		c2 = Instantiate (c2, spawnpoint2.transform.position, Quaternion.identity) as Creature;
		c2.currenttile = spawnpoint2;
		c2.x = mg.mapwidth - 1;
		c2.y = mg.mapwidth - 1;
		
	}
}

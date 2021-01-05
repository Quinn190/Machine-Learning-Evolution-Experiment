using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceEngine : MonoBehaviour {

	public float[] BestInputWeights;
	public float[] BestHiddenWeights;

	public Vector3 spawnpoint;

	public int numcreatures;
	public float roundtime;

	public Creature creature;
	public Creature[] creatures;


	public bool first = true;

	void Start ()
	{
		Load();
		StartRound();
		
	}


	void StartRound ()
	{
		SpawnCreatures();
	}

	void SpawnCreatures() 
	{
		for (int i = 0; i < numcreatures; i++)
		{
			creatures[i] = Instantiate(creature, spawnpoint, Quaternion.identity, transform);
		}
	}





	public void Load() 
	{
		Data data = SaveSystem.LoadState();

		BestInputWeights = data.BestInputWeights;
		BestHiddenWeights = data.BestHiddenWeights;
	}
}

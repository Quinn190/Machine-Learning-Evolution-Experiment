using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

	public float[] BestInputWeights;
	public float[] BestHiddenWeights;
	public float[] highestfitnessfunction;
	public int numBestRecords;

	public int creaturesactive;
	public Vector3 spawnpoint;

	public int numcreatures;
	public float roundtime;
	public float roundcap;
	public int generations;

	public Creature creature;
	public Creature[] creatures;
	public ScoreKeeper sk;

	public bool first = true;

	void Start ()
	{

		BestInputWeights = new float[410 * numBestRecords];
		BestHiddenWeights = new float[50 * numBestRecords];
		highestfitnessfunction = new float[numBestRecords];
		
		StartRound();
		
	}


	void StartRound ()
	{
		SpawnCreatures();
	}

	public void EndRound() {
		first = false;
		foreach (Transform child in transform)
		{
			Creature childcreature = child.GetComponent<Creature>();
			childcreature.Die();
		}
		roundtime = 0f;
		creature.time_score = 0;
		StartRound();
		generations++;
		
		highestfitnessfunction[2] = 0;
	}

	void Update ()
	{
		if (creaturesactive <=0 || roundtime > roundcap) 
		{
			EndRound();
		}

		roundtime += Time.deltaTime;
	}

	void SpawnCreatures ()
	{
		spawnpoint = new Vector3 (0,0,0);
		creatures = new Creature[numcreatures];
		creaturesactive = numcreatures;
		for (int i = 0; i < numcreatures; i++)
		{
			creatures[i] = Instantiate(creature, spawnpoint, Quaternion.identity, transform);
			creatures[i].active = true;
			creatures[i].mutationweight = i % 25;
			
			
			
			
		}

		
		
	}

	

	public void CheckHighest(float x, float[] inputweights, float[] hiddenweights, float dst, float time) 
	{	
		
		if (x > highestfitnessfunction[2]) 
		{
			if (x > highestfitnessfunction[1])
			{
				if (x > highestfitnessfunction[0])
				{	
					highestfitnessfunction[2] = highestfitnessfunction[1];	
					highestfitnessfunction[1] = highestfitnessfunction[0];
					highestfitnessfunction[0] = x;

					if (x > 50) 
					{	
						sk.times.Add(time);
						sk.gens.Add(generations);
						sk.distances.Add(dst);
					}





					for (int i = 0; i < 200; i++) 
					{
						BestInputWeights[400 + i] = BestInputWeights[200 + i];
						BestInputWeights[200 + i] = BestInputWeights[i];
						BestInputWeights[i] = inputweights[i];
					}
					for (int j = 0; j < 40; j++) 
					{	BestHiddenWeights[80 + j] = BestHiddenWeights[80 + j];
						BestHiddenWeights[40 + j] = BestHiddenWeights[40 + j];
						BestHiddenWeights[j] = hiddenweights[j];
					}
				}
				else 
				{
					highestfitnessfunction[2] = highestfitnessfunction[1];	
					highestfitnessfunction[1] = x;
					for (int i = 0; i < 200; i++) 
					{
						BestInputWeights[400 + i] = BestInputWeights[200 + i];
						BestInputWeights[200 + i] = inputweights[i];
						
					}
					for (int j = 0; j < 40; j++) 
					{	BestHiddenWeights[80 + j] = BestHiddenWeights[80 + j];
						BestHiddenWeights[40 + j] = hiddenweights[j];
					}
				}
			}
			else 
			{
				highestfitnessfunction[2] = x;	
				
				for (int i = 0; i < 200; i++) 
				{
					BestInputWeights[400 + i] = inputweights[i];
					
				}
				for (int j = 0; j < 40; j++) 
				{	BestHiddenWeights[80 + j] = hiddenweights[j];
				}

			}
			
		}
	}

	public void Save()
	{
		SaveSystem.SaveState(this);
	}

	public void Load() 
	{
		Data data = SaveSystem.LoadState();

		BestInputWeights = data.BestInputWeights;
		BestHiddenWeights = data.BestHiddenWeights;
	}
}

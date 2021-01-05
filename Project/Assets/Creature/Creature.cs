using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Creature : MonoBehaviour {

	public int score;
	public float dst_score;
	public float time_score;
	public float cmb_score;

	public float greencol;
	
	public bool active;

	public string mode = "Train";

	public float movespeed;

	public GameEngine GE;
	public RaceEngine RE;
	public Rigidbody rb;
	public Vision vision;

	public float[]  Inputs;
	public float[]  Inputweights;
	public float[]  Hidden;
	public float[]  Hiddenweights;
	public float[]  Outputs;
	public float    weightvalue = 4.0f;



	public int numInputs;
	public int numHidden;
	public int numOutputs;

	public int mutationweight;

	void Start () {
		rb = GetComponent<Rigidbody>();
		vision = GetComponent<Vision>();

		numInputs = 41;
		numHidden  = 10;
		numOutputs = 5;

		Inputs = vision.Inputs;

		Hidden        = new float[numHidden];
		Outputs       = new float[numOutputs];
		Inputweights  = new float[numInputs * numHidden];
		Hiddenweights = new float[numHidden * numOutputs];
		
		switch(mode)
		{
			case "Train":
				TrainSetup();
				break;
			case "Race":
				RaceSetup();
				break;
			default:
				break;
		}
		
		
		
	}


	void TrainSetup()
	{
		CalculateWeights();
		if (GE.first == false) 
		{
			MutateWeights();
		}
	}

	void RaceSetup()
	{
		LoadWeights();
	}

	void Update() 
	{	
		time_score += Time.deltaTime;
		if (active == true) {
			Inputs = vision.Inputs;
			Inputs[0] = rb.velocity.magnitude / 2;
			if (Inputs[0] >  1f) {
				Inputs[0] = 1f;
			}
			CalculateHidden();
			CalculateOutputs ();
		}

		

		if (transform.position.y <= -3) 
		{
			Die();
		}
	}

	void FixedUpdate()
	{
		Vector3 movement = vision.forward * movespeed * Outputs[0];

		rb.AddForce(movement);

	
	}


	public void CalculateHidden () {
		for (int j = 0; j < numHidden; j++) {
			Hidden[j] = 0;
			for (int i = 0; i < numInputs; i++) {
				Hidden[j] += Inputs[i] * Inputweights[(i * numHidden) + j];
				if (i == numInputs - 1) {
					Hidden[j] = SigmoidFunction(Hidden[j]);
				}
			}
		}
	}

	public void CalculateWeights () {
		if (GE.first == false)
		{	int random = Random.Range(0,3);
			for (int i = 0; i < numInputs * numHidden; i++) {
				Inputweights [i] = GE.BestInputWeights [numInputs * numHidden * random + i];
			}
			for (int j = 0; j < numHidden * numOutputs; j++) {
				Hiddenweights [j] = GE.BestHiddenWeights [numOutputs * numHidden * random + j];
			}
		}
		if (GE.first == true)
		{
			for (int i = 0; i < numInputs * numHidden; i++) {
				Inputweights [i] = Random.Range (-weightvalue, weightvalue);
			}
			for (int j = 0; j < numHidden * numOutputs; j++) {
				Hiddenweights [j] = Random.Range (-weightvalue, weightvalue);
			}
		}
	}

	public void LoadWeights () {
		if (GE.first == false)
		{	int random = Random.Range(0,3);
			for (int i = 0; i < numInputs * numHidden; i++) {
				Inputweights [i] = GE.BestInputWeights [numInputs * numHidden * random + i];
			}
			for (int j = 0; j < numHidden * numOutputs; j++) {
				Hiddenweights [j] = GE.BestHiddenWeights [numOutputs * numHidden * random + j];
			}
		}
		if (GE.first == true)
		{
			for (int i = 0; i < numInputs * numHidden; i++) {
				Inputweights [i] = Random.Range (-weightvalue, weightvalue);
			}
			for (int j = 0; j < numHidden * numOutputs; j++) {
				Hiddenweights [j] = Random.Range (-weightvalue, weightvalue);
			}
		}
	}

	public void CalculateOutputs () {
		for (int j = 0; j < numOutputs; j++) {
			Outputs[j] = 0;
			for (int i = 0; i < numHidden; i++) {
				Outputs[j] += Hidden[i] * Hiddenweights[(i * numOutputs) + j];
				if (i == numHidden - 1) {
					Outputs[j] = SigmoidFunction(Outputs[j]);
				}
			}
		}
	}

	

	
	

	float SigmoidFunction(float x) {
		float y;
		y = 1.0f / (1.0f + Mathf.Exp (-x));
		return y;
	}

	public void MutateWeights() {
		GenerateColor(mutationweight);
		for (int i = 0; i < numInputs * numHidden; i++) {
			if (Random.Range(1,100) > 100 - mutationweight){
				Inputweights [i] = Random.Range (-weightvalue, weightvalue);
			}
		}
		for (int k = 0; k < numHidden * numOutputs; k++) {
			if (Random.Range(1,100) > 100 - mutationweight){
				Hiddenweights [k] = Random.Range (-weightvalue, weightvalue);
			}
		}
	}

	public void Die() 
	{
		GE.creaturesactive -= 1;
		Destroy(gameObject);
		GE.CheckHighest(cmb_score, Inputweights, Hiddenweights, dst_score, time_score); 
	}

	void GenerateColor(int color) 
	{
		gameObject.GetComponent<Renderer>().material.color = new Color((float)color/25f, greencol, 1-(float)color/25f);
	}
}

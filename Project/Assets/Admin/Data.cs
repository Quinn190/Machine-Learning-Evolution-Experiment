using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data  {

	public float[] BestInputWeights;
	public float[] BestHiddenWeights;

	public Data(GameEngine GE)
	{
		BestInputWeights = GE.BestInputWeights;
		BestHiddenWeights = GE.BestHiddenWeights;
	} 
}

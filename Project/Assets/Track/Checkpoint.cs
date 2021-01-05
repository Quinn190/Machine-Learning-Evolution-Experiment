using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	public int score;

	public void OnTriggerEnter (Collider col) 
	{
		if(col.gameObject.tag == "Creature")
		{
			if (col.GetComponent<Creature>().dst_score < score) 
			{
				col.GetComponent<Creature>().dst_score = score;
				col.GetComponent<Creature>().cmb_score = (100 - col.GetComponent<Creature>().time_score)/100 + col.GetComponent<Creature>().dst_score;
			}
			
		}
	}
}

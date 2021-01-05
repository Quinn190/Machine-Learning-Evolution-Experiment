using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {
	public int speedboost;
	
	
	public void OnTriggerEnter (Collider col) 
	{
		if(col.gameObject.tag == "Creature")
		{
			col.GetComponent<Creature>().movespeed *= speedboost;
		}
	}

	public void OnTriggerExit (Collider col) 
	{
		if(col.gameObject.tag == "Creature")
		{
			col.GetComponent<Creature>().movespeed = 36;
		}
	}


	

	
}

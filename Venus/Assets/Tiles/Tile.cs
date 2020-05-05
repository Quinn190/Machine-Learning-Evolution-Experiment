using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	public int food;	
	
	public Color basecolor;
	public Color highlight;

	public int xcoord;
	public int ycoord;

	public ModeEngine ME;
	public MapGenerator MG;

	public string Roundlstring;

	public void Awake () {
		food = Random.Range(0,100);

	}

	void OnMouseOver()
    {
        if (ME.Mode == "Tile Mode") {
        	GetComponent<Renderer>().material.color = highlight;
        }
    }

    void OnMouseExit()
    {
        if (ME.Mode == "Tile Mode") {
        	GetComponent<Renderer>().material.color = basecolor;
        }
    }


}

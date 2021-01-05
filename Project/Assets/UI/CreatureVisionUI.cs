using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureVisionUI : MonoBehaviour {

	public int id = 0;
	public Vision vision;

	RawImage m_Graphic;
    Color m_MyColor;
    public Camera cam;

	void Update() {

		if (Input.GetMouseButtonDown(0)) 
		{	
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        	RaycastHit hit;
        	if (Physics.Raycast(ray, out hit, 100))
        	{	
        		vision = hit.collider.GetComponent<Vision>();
        		Debug.Log("hit");
        	}
		}

		if (vision != null)
		{
			if (vision.Inputs[id] == 1)
			{
				m_Graphic = GetComponent<RawImage>();
				m_Graphic.color = Color.white;
			}
			else 
			{
				m_Graphic = GetComponent<RawImage>();
				m_Graphic.color = Color.black;
			}
		}
		

	
	}
}

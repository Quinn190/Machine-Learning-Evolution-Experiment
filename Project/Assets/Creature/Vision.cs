using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {

	public Vector3[] dirs;
	public float[] Inputs;
	public Vector3 forward = Vector3.forward * 2;
	public Vector3 right = Vector3.right * 2
	;
	public float rotatespeed;

	public int xvision;
	public int zvision;
	void Start () {
		DrawVision();
		Inputs = new float[zvision*xvision * 2 + 1];
		Inputs[0] = 0f;

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawRay(transform.position, forward * 100, Color.red);
		//Debug.DrawRay(transform.position, right * 100, Color.yellow);
		for (int i = 0; i < xvision * zvision; i++)
		{
			int visionrays = xvision * zvision;
			/*if (i % 5 == 0)
			{
				Debug.DrawRay(transform.position, dirs[i] * 10, Color.green);
			}

			else if (i % 5 == 3)
			{
				Debug.DrawRay(transform.position, dirs[i] * 10, Color.red);
			}

			else if (i % 5 == 2)
			{
				Debug.DrawRay(transform.position, dirs[i] * 10, Color.yellow);
			}

			else 
			{
				Debug.DrawRay(transform.position, dirs[i] * 10, Color.blue);	
			}
			*/
			
			Inputs[i + 1] = TrackInVision(dirs[i]);
			Inputs[visionrays + i + 1] = BoosterInVision(dirs[i]);
		}
		if (GetComponent<Creature>().Outputs[3] > 0.7f){
			rotatespeed = ((GetComponent<Creature>().Outputs[1])-0.5f) / 5f;
		}
		else 
		{
			rotatespeed = 0f;
		}
		
		UpdateVision(rotatespeed);
		DrawVision();
	}



	void DrawVision() 
	{
		dirs = new Vector3[xvision * zvision];
		for (int i = 0; i < xvision; i++) 	
		{
			for (int j = 0; j < zvision; j++) 
			{
				dirs[zvision*i+j] = (i+1) * forward + (j-(zvision-1)/2) * right + Vector3.down * 0.5f;
			}
		}
	}

	void UpdateVision(float rotation)
	{
		Vector3 fright = new Vector3(forward.z, 0f, -forward.x) * rotation;
		forward += fright;
		forward.Normalize();
		right = new Vector3(forward.z, 0f, -forward.x);
		
	}


	//
	public float TrackInVision(Vector3 dir) 
	{	int mask = (1 << 10);
		RaycastHit hit;
		Ray ray = new Ray(transform.position, dir);
		if (Physics.Raycast (ray, out hit, 10, mask)) {
			if (hit.collider.tag == "Track" || hit.collider.tag == "Booster")
			{
				return 1f;
			}

			else 
			{
				return 0f;
			}
		}
		else 
		{
			return 0f;
		}
	}
	public float BoosterInVision(Vector3 dir) 
	{	int mask = (1 << 10);
		RaycastHit hit;
		Ray ray = new Ray(transform.position, dir);
		if (Physics.Raycast (ray, out hit, 10, mask)) {
			if (hit.collider.tag == "Booster")
			{
				return 1f;
			}

			else 
			{
				return 0f;
			}
		}
		else 
		{
			return 0f;
		}
	}


}

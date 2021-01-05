using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCreator : MonoBehaviour {

	public Vector3[] points;
	public Vector3[] anchors;
	public Vector3[] controls;
	public string[] tracksegments;
	public int[] segmentlengths;
	public int segment;
	public int trackpieces;

	public Vector3 start; 


	public Vector3 forward;
	public Vector3 left;

	void Awake()
	{
		GenerateTrack();
	}


	public static Vector3 EvaluateQuadratic(Vector3 a, Vector3 b, Vector3 c, float t) 
	{
		Vector3 p0 = Vector3.Lerp(a, b, t);
		Vector3 p1 = Vector3.Lerp(b, c, t);
		return Vector3.Lerp(p0, p1, t);
	}
	public static Vector3 EvaluateCubic(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t) 
	{
		Vector3 p0 = EvaluateQuadratic(a, b, c, t);
		Vector3 p1 = EvaluateQuadratic(b, c, d, t);
		return Vector3.Lerp(p0, p1, t);
	}

	void GenerateTrack() 
	{
		trackpieces = tracksegments.Length;
		anchors = new Vector3[trackpieces + 1];
		controls = new Vector3[trackpieces];
	
		anchors[0] = start;


		points = new Vector3[segment * controls.Length];
		for (int i = 0; i < trackpieces; i++) 
		{
			switch(tracksegments[i])
			{
				case "Straight":
					anchors[i + 1] = anchors[i] + forward * segmentlengths[i];
					controls[i] = (anchors[i + 1] + anchors[i])/2;
					break;
				case "Left":
					anchors[i + 1] = anchors[i] + forward * segmentlengths[i] + left * segmentlengths[i];
					controls[i] = (anchors[i] + forward * segmentlengths[i]);
					
					break;
				case "Right":
					anchors[i + 1] = anchors[i] + forward * segmentlengths[i] - left * segmentlengths[i];
					controls[i] = (anchors[i] + forward * segmentlengths[i]);
					
					break;
				case "Ramp":
					anchors[i + 1] = anchors[i] + (forward + Vector3.up * 0.4f) * segmentlengths[i];
					controls[i] = anchors[i] + forward/2* segmentlengths[i];
					break;
				case "Ramp End":
					anchors[i + 1] = anchors[i] + (forward + Vector3.up * 0.4f) * segmentlengths[i];
					controls[i] = anchors[i] + (anchors[i] - controls[i - 1])/segmentlengths[i-1] * segmentlengths[i];	
					break;
				case "Down Ramp End":
					anchors[i + 1] = anchors[i] + (forward - Vector3.up * 0.4f) * segmentlengths[i];
					controls[i] = anchors[i] + (anchors[i] - controls[i - 1])/segmentlengths[i-1] * segmentlengths[i];
					break;
				case "Down Ramp":
					anchors[i + 1] = anchors[i] + (forward - Vector3.up * 0.4f) * segmentlengths[i];
					controls[i] = anchors[i] + forward/2* segmentlengths[i];
					break;
				default: 
					break;	


			}
			for (int j = 0; j < segment; j++)
			{
				points[i * segment + j] = EvaluateQuadratic(anchors[i], controls[i], anchors[i + 1], j/(float)segment);
			}

			forward = points[segment * i + segment- 1] - points[segment * i + segment - 2];
			forward = new Vector3(forward.x, 0, forward.z);
			forward.Normalize();
			left = new Vector3(-forward.z,0, forward.x);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackSegment : MonoBehaviour {
	[Range(0.05f, 1.5f)]
	public float spacing = 1;
	public float leftroadWidth = 5;
	public float rightroadWidth = 5;
	public Vector3[] points;
	int length;
	int width;

	void Start () 
	{
		UpdateTrack();
	}

	public void UpdateTrack()
	{	
		
		
		GetComponent<MeshFilter>().mesh = TrackMesh(points);
		
	}

	Mesh TrackMesh(Vector3[] points) 
	{
		Vector3[] verts = new Vector3[points.Length * 2];
		Vector2[] uvs = new Vector2[verts.Length];
		int[] tris = new int[2*(points.Length - 1) * 3];
		int vertIndex = 0;
		int triIndex = 0;

		for (int i = 0; i < points.Length; i++)
		{
			Vector3 forward = Vector3.zero;
			if (i < points.Length - 1)
			{
				forward += points [i + 1] - points[i];
			}
			if (i > 0) 
			{
				forward += points [i] - points [i - 1];
			}
			forward.Normalize();
			Vector3 left = new Vector3(-forward.z,0, forward.x);

			verts[vertIndex] = points[i] + left * leftroadWidth;
			verts[vertIndex + 1] = points[i] - left *rightroadWidth;

			float completionPercent = i/(float)(points.Length - 1);
			uvs[vertIndex] = new Vector2(0, completionPercent);
			uvs[vertIndex + 1] = new Vector2(1, completionPercent);

			if (i < points.Length - 1) 
			{
				tris[triIndex] = vertIndex;
				tris[triIndex + 1] = vertIndex + 2;
				tris[triIndex + 2] = vertIndex + 1;

				tris[triIndex + 3] = vertIndex + 1;
				tris[triIndex + 4] = vertIndex + 2;
				tris[triIndex + 5] = vertIndex + 3;
			} 

			vertIndex += 2;
			triIndex += 6;
		}

		Mesh mesh = new Mesh();
		mesh.vertices = verts;
		mesh.triangles = tris;
		mesh.uv = uvs;
		
		MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
		meshc.sharedMesh = mesh;
		return mesh;
	}
}

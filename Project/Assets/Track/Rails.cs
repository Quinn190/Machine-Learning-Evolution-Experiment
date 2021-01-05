using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour {
	public Vector3[] points;
	public float roadWidth = 5f;
	void Start () {
		
	}

	public void GenerateMesh()
	{	
		GetComponent<MeshFilter>().mesh = TrackMesh(points);
	}
	
	Mesh TrackMesh(Vector3[] points) 
	{
		Vector3[] verts = new Vector3[points.Length * 4];
		Vector2[] uvs = new Vector2[verts.Length];
		int[] tris = new int[2*(points.Length - 1) * 9];
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
			left.Normalize();


			verts[vertIndex] = points[i] + left * roadWidth * .5f - Vector3.up;
			verts[vertIndex + 1] = points[i] + left *roadWidth *.5f;

			verts[2 * points.Length + vertIndex] = points[i] - left * roadWidth * .5f;
			verts[2 * points.Length +  vertIndex + 1] = points[i] - left *roadWidth *.5f  - Vector3.up;

			float completionPercent = i/(float)(points.Length - 1);
			uvs[vertIndex] = new Vector2(0, completionPercent);
			uvs[vertIndex + 1] = new Vector2(1, completionPercent);

			uvs[2 * points.Length + vertIndex] = new Vector2(0, completionPercent);
			uvs[2 * points.Length + vertIndex + 1] = new Vector2(1, completionPercent);

			if (i < points.Length - 1) 
			{
				tris[triIndex] = vertIndex;
				tris[triIndex + 1] = vertIndex + 2;
				tris[triIndex + 2] = vertIndex + 1;

				tris[triIndex + 3] = vertIndex + 1;
				tris[triIndex + 4] = vertIndex + 2;
				tris[triIndex + 5] = vertIndex + 3;

				tris[9 * points.Length +  triIndex] = 2 * points.Length + vertIndex;
				tris[9 * points.Length +  triIndex + 1] = 2 * points.Length + vertIndex + 2;
				tris[9 * points.Length +  triIndex + 2] = 2 * points.Length + vertIndex + 1;

				tris[9 * points.Length + triIndex + 3] = 2 * points.Length + vertIndex + 1;
				tris[9 * points.Length + triIndex + 4] = 2 * points.Length + vertIndex + 2;
				tris[9 * points.Length +  triIndex + 5] = 2 * points.Length + vertIndex + 3;
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

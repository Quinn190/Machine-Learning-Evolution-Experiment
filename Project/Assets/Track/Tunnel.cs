using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel : MonoBehaviour {

	public Vector3[] points;
	public float innerradius;
	public float outerradius;
	public int segments;
	public string inside;

	public TunnelRings ring;	
	public int spacing;
	public float distance;

	public Vector3[] verts;
	public int[] tris;
	void Start() 
	{
		GenerateMesh();
	}

	public void GenerateMesh()
	{	
		CalculateDistance(points);
		switch(inside)
		{
			case "Inside":
				GetComponent<MeshFilter>().mesh = InsideTunnelMesh(points);
				break;
			case "Outside":
				GetComponent<MeshFilter>().mesh = OutsideTunnelMesh(points);
				break;
			default:
				break;
		}

		GenerateRings();
		
	}
	//Inside Tunnel
	Mesh InsideTunnelMesh(Vector3[] points) 
	{
		Vector3[] verts = new Vector3[(points.Length - 1) * segments ];
		Vector2[] uvs = new Vector2[verts.Length];
		int[] tris = new int[(points.Length - 2) * segments * 2 * 3];
		int vertIndex = 0;
		int triIndex = 0;
		float tunneldistance = 0;

		//Generate circle for each point
		for (int i = 1; i < points.Length; i ++)
		{
			Vector3 forward = points[i] - points[i-1];
			Vector3 left = new Vector3(forward.z, 0, -forward.x);
			left.Normalize();
			for (int j = 0; j < segments; j++)
			{
				float x = Mathf.Cos((2 * Mathf.PI * j)/segments) * innerradius;
				float y = Mathf.Sin((2 * Mathf.PI * j)/segments) * innerradius;

				verts[vertIndex] = points[i] + x * left + y * Vector3.up;

				uvs[vertIndex] = new Vector2(j / (segments-1), tunneldistance/distance);
				if (i < points.Length - 1) 
				{
					if (j < segments - 1) 
					{
						tris[triIndex] = vertIndex;
						tris[triIndex + 1] = vertIndex + segments;
						tris[triIndex + 2] = vertIndex + segments + 1;

						tris[triIndex + 3] = vertIndex;
						tris[triIndex + 4] = vertIndex + segments + 1;
						tris[triIndex + 5] = vertIndex + 1;
					}

					else if (j == segments - 1)
					{
						tris[triIndex] = vertIndex;
						tris[triIndex + 1] = vertIndex + segments;
						tris[triIndex + 2] = vertIndex + 1;

						tris[triIndex + 3] = vertIndex;
						tris[triIndex + 4] = vertIndex + 1;
						tris[triIndex + 5] = vertIndex + 1 - segments;
					}
					
				}

				vertIndex +=1;
				triIndex +=6;
			}
			tunneldistance += (points[i] - points[i-1]).magnitude;
		}
		Mesh mesh = new Mesh();
		mesh.vertices = verts;
		mesh.triangles = tris;
		mesh.uv = uvs;
		
		MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
		meshc.sharedMesh = mesh;
		return mesh;
	}

	Mesh OutsideTunnelMesh(Vector3[] points) 
	{
		Vector3[] verts = new Vector3[(points.Length - 1) * segments ];
		Vector2[] uvs = new Vector2[verts.Length];
		int[] tris = new int[(points.Length - 2) * segments * 2 * 3];
		int vertIndex = 0;
		int triIndex = 0;
		float tunneldistance = 0;

		//Generate circle for each point
		for (int i = 1; i < points.Length; i ++)
		{
			Vector3 forward = points[i] - points[i-1];
			Vector3 left = new Vector3(forward.z, 0, -forward.x);
			left.Normalize();
			for (int j = 0; j < segments; j++)
			{
				float x = Mathf.Cos((2 * Mathf.PI * j)/segments) * outerradius;
				float y = Mathf.Sin((2 * Mathf.PI * j)/segments) * outerradius;

				verts[vertIndex] = points[i] + x * left + y * Vector3.up;

				uvs[vertIndex] = new Vector2(j / (segments-1), tunneldistance/distance);
				if (i < points.Length - 1) 
				{
					if (j < segments - 1) 
					{
						tris[triIndex] = vertIndex;
						tris[triIndex + 2] = vertIndex + segments;
						tris[triIndex + 1] = vertIndex + segments + 1;

						tris[triIndex + 3] = vertIndex;
						tris[triIndex + 5] = vertIndex + segments + 1;
						tris[triIndex + 4] = vertIndex + 1;
					}

					else if (j == segments - 1)
					{
						tris[triIndex] = vertIndex;
						tris[triIndex + 2] = vertIndex + segments;
						tris[triIndex + 1] = vertIndex + 1;

						tris[triIndex + 3] = vertIndex;
						tris[triIndex + 5] = vertIndex + 1;
						tris[triIndex + 4] = vertIndex + 1 - segments;
					}
					
				}

				vertIndex +=1;
				triIndex +=6;
			}
			tunneldistance += (points[i] - points[i-1]).magnitude;
		}
		Mesh mesh = new Mesh();
		mesh.vertices = verts;
		mesh.triangles = tris;
		mesh.uv = uvs;
		
		MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
		meshc.sharedMesh = mesh;
		return mesh;
	}

	void CalculateDistance(Vector3[] points) 
	{	distance = 0;
		for (int i = 1; i < points.Length; i++)
		{
			distance += (points[i] - points[i-1]).magnitude;
		}
	}

	void GenerateRings()
	{
		for (int i = 0; i < spacing; i++)
		{
			
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMeshCreator : MonoBehaviour {
	[Range(0.05f, 1.5f)]
	public float spacing = 1;
	public float leftWidth = 1;
	public float roadWidth = 1;

	public Checkpoint cp;
	public Booster bst;
	public Rails rails;
	public Tunnel tunnel;
	public Tunnel[] tunnels;

	public int[] tunnel_starts;
	public int[] tunnel_lengths;
	


	void Start () 
	{
		UpdateTrack();
		GenerateCheckpoints();
		GenerateRails();
		GenerateTunnels();
	}

	public void UpdateTrack()
	{	
		Vector3[] points = GetComponent<TrackCreator>().points;
		
		GetComponent<MeshFilter>().mesh = TrackMesh(points);

		
	}

	Mesh TrackMesh(Vector3[] points) 
	{
		Vector3[] verts = new Vector3[points.Length * 2];
		Vector2[] uvs = new Vector2[verts.Length];
		int[] tris = new int[2*(points.Length - 1) * 6];
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

			verts[vertIndex] = points[i] + left * roadWidth * .5f;
			verts[vertIndex + 1] = points[i] - left *roadWidth *.5f;

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

	void GenerateCheckpoints()
	{
		Vector3[] points = GetComponent<TrackCreator>().points;
		int segments = GetComponent<TrackCreator>().segment;
		for (int i = 0; i < points.Length - 1; i++)
		{
			Vector3 forward = Vector3.zero;
			forward = points[i+1] - points[i];
			Vector3 left = new Vector3(-forward.z,0, forward.x);
			Quaternion rotation = Quaternion.LookRotation(left, -forward);
			if (i % (segments/10) == 0)
			{
				cp = Instantiate(cp, points[i] + Vector3.up * 2, rotation, transform);
				
				cp.score = i/10;
			}
		}
	}

	void GenerateRails() 
	{
		Vector3[] points = GetComponent<TrackCreator>().points;
		rails = Instantiate(rails, points[0], Quaternion.identity, transform);
		rails.points = points;
		rails.roadWidth = roadWidth;
		rails.GenerateMesh();
	}

	void GenerateTunnels()
	{
		Vector3[] points = GetComponent<TrackCreator>().points;
		for (int i = 0; i < tunnels.Length; i++)
		{
			tunnel = Instantiate(tunnel, points[0], Quaternion.identity, transform);
			tunnel.points = tunnelpoints(tunnel_starts[i], tunnel_lengths[i]);
			tunnel.inside = "Inside";
			tunnel.GenerateMesh();

			tunnel = Instantiate(tunnel, points[0], Quaternion.identity, transform);
			tunnel.points = tunnelpoints(tunnel_starts[i], tunnel_lengths[i]);
			tunnel.inside = "Outside";
			tunnel.GenerateMesh();
		}
	}

	Vector3[] tunnelpoints(int start, int length)
	{
		Vector3[] tpoints = new Vector3[length];
		Vector3[] points = GetComponent<TrackCreator>().points;
		for (int i = 0; i < length; i++)
		{
			tpoints[i] = points[i + start];
		}
		return tpoints;
		
	}	
}

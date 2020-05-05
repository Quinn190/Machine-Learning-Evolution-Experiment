using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]


public class MapGenerator : MonoBehaviour {
	
	public int redpct;
	public int greenpct;

	public Tile spawntile1;
	public Tile spawntile2;

	//List of Tilestypes
	public Tile[] TileTypes;
	public Tile[,] Tilelist;
	public int mapwidth;
	public Tile m;
	public Tile k;
	public Vector3 mapv;
	int j;
	int c;

	void Awake (){
		GenerateMap();
	}

	public void GenerateMap(){
		k = Instantiate (TileTypes[3], Vector3.zero , Quaternion.identity) as Tile;
		k.gameObject.transform.localScale = new Vector3(mapwidth, 0.2f, mapwidth);
		k.gameObject.transform.parent = gameObject.transform;
		Tilelist = new Tile[mapwidth,mapwidth];
		for (int x = 0; x < mapwidth; x++) {
			for (int y = 0; y < mapwidth; y++) {
				mapv = new Vector3((x - (mapwidth-1.0f)/2.0f),0,(y-(mapwidth-1.0f)/2.0f));
					
				if (x == 0 && y == 0) {
					m = Instantiate (TileTypes[0], mapv, Quaternion.identity) as Tile;
					Tilelist[x,y] = m;
					spawntile1 = m;
					m.xcoord = x;
					m.ycoord = y;
				}
				else if (x == mapwidth-1 && y == mapwidth-1) {
					m = Instantiate (TileTypes[1], mapv, Quaternion.identity) as Tile;
					Tilelist[x,y] = m;
					spawntile2 = m;
					m.xcoord = x;
					m.ycoord = y;
				}
				else {
					m = Instantiate (TileTypes[2], mapv, Quaternion.identity) as Tile;
					Tilelist[x,y] = m;
					m.xcoord = x;
					m.ycoord = y;
				}

				
				m.gameObject.transform.parent = gameObject.transform;
				
				

			}
		}
	}	
}


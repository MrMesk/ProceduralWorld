using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterPerlinGlobal : MonoBehaviour
{
    //----For testing----
    public int height;
	public int width;
    Vector3 origin;
    //----For testing----
   
    public float scale;                             // Scale of the perlin noise
    public float heightScale;                       // Height of the perlin noise 
    public float speed;
    public float decal;


    public Transform waters;                        // Empty GameObject with all the water spawned
	public GameObject water;

	public List<Transform> my_Waters;               // List of all water spawned
    public Vector3[] vertices = new Vector3[20];    // Array which stock the vertex position of the mesh
	
	void Start () 
	{
        // Spawn a bunch of Water
		for (int i = 0; i < width; i++) 
		{
			origin = new Vector3(i * 10,0,origin.z);
			for (int k = 0; k < height; k++)
			{
				origin = new Vector3(origin.x ,0, k * 10);
				GameObject go = Instantiate(water,origin,Quaternion.identity) as GameObject;
				go.transform.SetParent(waters,true);
                my_Waters.Add (go.transform);           // Add the last water spawned in the list "my_Waters"
			}
		}
	}

	void Update () 
	{
        //----------Aply a perlin noise on every vertex of every mesh of waters spawned-----------
		if (my_Waters.Count > 0) 
		{
			for (int p = 0; p < my_Waters.Count; p++) 
			{
				vertices = my_Waters[p].GetComponent<MeshFilter> ().mesh.vertices;

				for (int u = 0; u < vertices.Length; u++) 
				{
                    vertices[u] = my_Waters[p].TransformPoint(my_Waters[p].GetComponent<MeshFilter>().mesh.vertices[u]);            //Change the vertex coordonate from local to world
                    vertices[u].y = heightScale * Mathf.PerlinNoise((vertices[u].x * scale) - Time.time/speed, (vertices[u].z * scale) + Time.time/speed);    //Aply perlin noise
                    vertices[u] = my_Waters[p].InverseTransformPoint(vertices[u]);                                                  //Bring back the vertex changes to local
                }

                my_Waters[p].transform.GetComponent<MeshFilter> ().mesh.vertices = vertices;                                    
                my_Waters[p].transform.GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
                my_Waters[p].transform.GetComponent<MeshFilter> ().mesh.RecalculateNormals ();

                //my_Go[p].GetComponent<MeshCollider>().sharedMesh = null;                                                      // Aply the mesh change on the collider
                //my_Go[p].GetComponent<MeshCollider>().sharedMesh = my_Go[p].transform.GetComponent<MeshFilter>().mesh;
            }
		}
	}
}

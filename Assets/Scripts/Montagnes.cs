using UnityEngine;
using System.Collections;

public class Montagnes : MonoBehaviour {
	private float decalX, decalZ;
	public float height;

	// Use this for initialization
	void Start () {
		decalX = Random.Range (0f, 100f);
		decalZ = Random.Range (0f, 100f);
		height = Mathf.Sqrt (height);
		Mesh mesh = GetComponent<MeshFilter> ().mesh;
		Vector3[] v = mesh.vertices;
		for (int i = 0; i< 11; ++i) {
			for (int j = 0; j< 11; ++j) {
				int pos = i*11+j;
				float heightPerlin = Mathf.PerlinNoise(v[pos].x + decalX, v[pos].z + decalZ);
				if (i < 10/2)
				{
					heightPerlin *= i*2*height/10;
				}
				else
				{
					heightPerlin *= (2*height-i*2*height/10);
				}
				if (j < 10/2)
				{
					heightPerlin *= j*2*height/10;
				}
				else
				{
					heightPerlin *= (2*height-j*2*height/10);
				}
				v[pos] = new Vector3(v[pos].x, heightPerlin, v[pos].z);
			}
		}
		mesh.vertices = v;
		mesh.RecalculateNormals ();
		mesh.RecalculateBounds ();
	}

}

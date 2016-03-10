using UnityEngine;
using System.Collections;

public class WaterPerlin : MonoBehaviour
{
    private Vector3[] my_Vertices;
    private Vector3[] my_Normals;
    private Mesh my_Mesh;

    public float my_ScaleA = 0.2f;              // Scale of the perlin noise first layer
    public float my_ScaleB = 2;                 // Scale of the perlin noise second layer
    public float my_HeightScaleA = 0.5f;        // Height of the perlin noise first layer
    public float my_HeightScaleB = 0.5f;        // Height of the perlin noise second layer
    float my_Speed = 1f;                        // Speed of the perlin noise

    void Start() {

        my_Mesh = GetComponent<MeshFilter>().mesh;
        my_Vertices = my_Mesh.vertices;
        my_Normals = my_Mesh.normals;

    }

    void Update ()
    {
        for (int u = 0; u < my_Vertices.Length; u++)
       {
        my_Normals[u] = transform.up;
        my_Vertices[u] = transform.TransformPoint(my_Mesh.vertices[u]);                                                                                 //Change the vertex coordonate from local to world
        my_Vertices[u].y =
                transform.position.y
                + my_HeightScaleA * Mathf.PerlinNoise((my_Vertices[u].x * my_ScaleA) - Time.time * my_Speed, (my_Vertices[u].z * my_ScaleA) + Time.time * my_Speed)
                + my_HeightScaleB * Mathf.PerlinNoise((my_Vertices[u].x * my_ScaleB) - Time.time * my_Speed, (my_Vertices[u].z * my_ScaleB) + Time.time * my_Speed)
                ;    //Aply perlin noise
        my_Vertices[u] = transform.InverseTransformPoint(my_Vertices[u]);                                                                               //Bring back the vertex changes to local
       }
       
       my_Mesh.vertices = my_Vertices;
       my_Mesh.normals = my_Normals;
       my_Mesh.RecalculateBounds();
    }
}

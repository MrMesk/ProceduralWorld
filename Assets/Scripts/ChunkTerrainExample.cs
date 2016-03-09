using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ChunkTerrainExample : MonoBehaviour {

    // 2 Axis Biome Matrix
    Dictionary<int, Dictionary<int, Chunk.Biome>> dictionary;

    public float size = 1;

    void Start() {
        // Scale the terrain
        transform.localScale = new Vector3(size, 1, size);

        // Generate the terrain's mesh
        Mesh mesh = Chunk.Terrain.GenerateMesh((int)size, (int)size, ExampleBiomeMatrixTest());

        // Edit the mesh by applying a height modifier based on biomes
        Chunk.Terrain.ApplyBiomeHeightModification(ref mesh);

        // Apply the mesh by setting it in the MeshFilter
        GetComponent<MeshFilter>().mesh = mesh;
    }

    // Example of a Biome Matrix used to generate this terrain
    Chunk.Biome[,] ExampleBiomeMatrixTest() {
        return new Chunk.Biome[,] {
            { (Chunk.Biome)3, (Chunk.Biome)3, (Chunk.Biome)3 },
            { (Chunk.Biome)4, (Chunk.Biome)4, (Chunk.Biome)4 },
            { (Chunk.Biome)3, (Chunk.Biome)3, (Chunk.Biome)3 }
        };
    }

    // Example of a Biome Matrix used to generate this terrain
    Chunk.Biome[,] ExampleBiomeMatrixTest() {
        return new Chunk.Biome[,] {
            { (Chunk.Biome)3, (Chunk.Biome)3, (Chunk.Biome)3 },
            { (Chunk.Biome)4, (Chunk.Biome)4, (Chunk.Biome)4 },
            { (Chunk.Biome)3, (Chunk.Biome)3, (Chunk.Biome)3 }
        };
    }

    /** BIOMEMATRIX NOMENCLATURE EXAMPLES **/
    // Chunk.Terrain.GenerateBiomeMatrix (
    //      Biome topLeft,      Biome top,      Biome topRight,
    //      Biome left,         Biome center,   Biome right,
    //      Biome bottomLeft,   Biome bottom,   Biome bottomRight
    // )

    Chunk.Biome[,] ExampleBiomeMatrix1() {
        return Chunk.Terrain.GenerateBiomeMatrix(
            Chunk.Biome.MOUNTAIN, Chunk.Biome.GRASSLAND, Chunk.Biome.GRASSLAND,
            Chunk.Biome.GRASSLAND, Chunk.Biome.GRASSLAND, Chunk.Biome.BEACH,
            Chunk.Biome.GRASSLAND, Chunk.Biome.BEACH, Chunk.Biome.SEA
        );
    }

    Chunk.Biome[,] ExampleBiomeMatrix2() {
        return new Chunk.Biome[,] {
            { Chunk.Biome.MOUNTAIN,     Chunk.Biome.GRASSLAND,  Chunk.Biome.GRASSLAND   },
            { Chunk.Biome.GRASSLAND,    Chunk.Biome.GRASSLAND,  Chunk.Biome.BEACH       },
            { Chunk.Biome.GRASSLAND,    Chunk.Biome.BEACH,      Chunk.Biome.SEA         }
        };
    }

    Chunk.Biome[,] ExampleBiomeMatrix3() {
        return new Chunk.Biome[,] {
            { (Chunk.Biome)4, (Chunk.Biome)3, (Chunk.Biome)3 },
            { (Chunk.Biome)3, (Chunk.Biome)3, (Chunk.Biome)2 },
            { (Chunk.Biome)3, (Chunk.Biome)2, (Chunk.Biome)1 }
        };
    }

}

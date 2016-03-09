using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chunk {

    [RequireComponent(typeof(BoxCollider))]
    public class Chunk : MonoBehaviour {
        // Global Biome Matrix
        public static Dictionary<int, Dictionary<int, Biome>> globalBiomeMatrix = new Dictionary<int, Dictionary<int, Biome>>();

        // Global Chunk Matrix
        public static Dictionary<int, Dictionary<int, Chunk>> globalChunkMatrix = new Dictionary<int, Dictionary<int, Chunk>>();

        static ChunkTerrainExample chunkManager;
        public GameObject prefabTerrain;
        public GameObject prefabOcean;

        public int x;
        public int y;
        GameObject terrain;
        static int terrainResolution;

        // Reset globals
        public static void ResetGlobals() {
            // Reset global biome matrix
            globalBiomeMatrix = new Dictionary<int, Dictionary<int, Biome>>();

            // Reset global chunk matrix
            globalChunkMatrix = new Dictionary<int, Dictionary<int, Chunk>>();
        }

        // Handle chunk configuration
        void Load(int _x, int _y) {
            // Configure the chunk
            x = _x;
            y = _y;

            // Preload biome matrix

            // Generate chunk's terrain
            //GenerateTerrain(TerrainUtility.GenerateBiomeMatrix(globalBiomeMatrix, _x, _y);

            // Act depending of the chunk's biome
            /*
            switch (_biomeMatrix[1, 1]) {
                case Biome.SEA:
                    break;
                case Biome.BEACH:
                    break;
                case Biome.GRASSLAND:
                    break;
                case Biome.MOUNTAIN:
                    break;
            }*/
        }

        // Handle terrain generation
        void GenerateTerrain(Biome[,] _biomeMatrix) {
            GameObject terrain = Instantiate(prefabTerrain) as GameObject;
            terrain.transform.parent = transform.parent;
            terrain.transform.position = Vector3.zero;
            terrain.GetComponent<Terrain>().Set(_biomeMatrix, terrainResolution);
        }

        // Handle player detection
        void OnTriggerEnter(Collider other) {
            if (other.tag == "Player") {
                chunkManager.ChunkTriggerCalledAt(x, y);
            }
        }

    }

}
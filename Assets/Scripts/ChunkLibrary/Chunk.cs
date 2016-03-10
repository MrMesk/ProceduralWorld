using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chunk {

    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class Chunk : MonoBehaviour {
        // Global Biome Matrix
        public static Dictionary<int, Dictionary<int, Biome>> globalBiomeMatrix = new Dictionary<int, Dictionary<int, Biome>>();

        // Global Chunk Matrix
        public static Dictionary<int, Dictionary<int, Chunk>> globalChunkMatrix = new Dictionary<int, Dictionary<int, Chunk>>();

        // Reference toward the chunk manager
        static ChunkTerrainExample chunkManager;

        // Prefab storage
        public GameObject prefabChunk;
        public GameObject prefabTerrain;
        public GameObject prefabSea;

        // Global data
        static Vector2 spacing;         // spacing between chunks
        static int terrainResolution;   // terrain's resolution in tiles
        static Vector3 terrainScale;    // terrain's scale

        // Instance data
        public int x;                   // chunk's x position
        public int y;                   // chunk's y position
        GameObject terrain;             // chunk's reference towards associated terrain game object

        // Reset globals
        public static void Init(ChunkTerrainExample _chunkManager) {
            // Reset global biome matrix
            globalBiomeMatrix.Clear();
            // Reset global chunk matrix
            globalChunkMatrix.Clear();

            // Set the chunk manager
            chunkManager = _chunkManager;

            // Set global data
            spacing = new Vector2(10, 10);
            terrainResolution = 16;
            terrainScale = new Vector3(10, 2.5f, 10);
        }

        public static GameObject CreateAt(GameObject prefabChunk, int _x, int _y) {
            GameObject output = Instantiate(prefabChunk);
            output.name = prefabChunk.name;
            output.transform.parent = chunkManager.transform;
            output.transform.position = new Vector3(_x * spacing.x, 0, _y * spacing.y);
            Chunk chunkInstance = output.GetComponent<Chunk>();
            chunkInstance.Load(_x, _y);
            return null;
        }

        // Handle chunk configuration
        public void Load(int _x, int _y) {
            // Configure the chunk
            x = _x;
            y = _y;

            // Preload biome matrix
            // Load surounding biomes if necessary
            Dictionary<int, Biome> biomeYAxis;
            for (_x = x -1; _x <=  x + 1; _x++) {
                for (_y = y - 1; _y <= y + 1; _y++) {
                    if (!globalBiomeMatrix.TryGetValue(_x, out biomeYAxis) || !biomeYAxis.ContainsKey(_y)) {
                        // Load biome if it wasn't loaded until now
                        BiomeUtility.LoadBiome(globalBiomeMatrix, _x, _y);
                    }
                }
            }

            // Generate chunk's terrain
            GenerateTerrain(TerrainUtility.GenerateBiomeMatrix(globalBiomeMatrix, x, y));

            // Act depending of the chunk's biome
            switch (globalBiomeMatrix[x][y]) {
                case Biome.SEA:
                    GenerateSea();
                    break;
                case Biome.BEACH:
                    GenerateSea();
                    break;
                case Biome.GRASSLAND:
                    break;
                case Biome.MOUNTAIN:
                    break;
            }

            // Set chunk trigger
            gameObject.GetComponent<BoxCollider>().size = new Vector3(terrainScale.x, 16, terrainScale.z);

            // Save Chunk
            Dictionary<int, Chunk> chunkYAxis;
            Chunk chunkFound;
            if (!globalChunkMatrix.TryGetValue(x, out chunkYAxis)) {     //X
                chunkYAxis = new Dictionary<int, Chunk>();
                globalChunkMatrix.Add(x, chunkYAxis);
            }
            if (chunkYAxis.TryGetValue(y, out chunkFound)) {             //Y
                chunkYAxis[y] = this;
            } else {
                chunkYAxis.Add(y, this);
            }
        }

        // Handle terrain generation
        void GenerateTerrain(Biome[,] _biomeMatrix) {
            GameObject instance = Instantiate(prefabTerrain) as GameObject;
            instance.name = prefabTerrain.name;
            instance.transform.parent = transform;
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = terrainScale;
            Terrain terrain = instance.GetComponent<Terrain>();
            terrain.Set(_biomeMatrix, terrainResolution, this);
        }

        // Handle terrain generation
        void GenerateSea() {
            GameObject instance = Instantiate(prefabSea) as GameObject;
            instance.name = prefabSea.name;
            instance.transform.parent = transform;
            instance.transform.localPosition = Vector3.up * -8f ;
            instance.transform.localScale = Vector3.one;
        }

        bool triggered = false;

        // Handle player detection
        void OnTriggerEnter(Collider other) {
            if (!triggered && other.tag == "Player") {
                triggered = true;
                chunkManager.ChunkTriggerCalledAt(x, y);
            }
        }



    }

}
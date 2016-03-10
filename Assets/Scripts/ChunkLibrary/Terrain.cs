using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Chunk namespace containing all chunk related programing
namespace Chunk {

    /** TERRAIN SCRIPT **/
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class Terrain : MonoBehaviour {

        // Local biome matrix indicating surounding chunk's biome
        Biome[,] biomeMatrix = null;
        // Resolution in polygone of the generated terrain
        public int resolution = 1;

        Chunk parent;

        // Configure chunk before loading it
        public void Set(Biome[,] _biomeMatrix, int _resolution, Chunk _parent) {
            biomeMatrix = _biomeMatrix;
            resolution = _resolution;
            parent = _parent;
        }

        // Load chunk at script's start
        void Start() {
            // Generate the terrain's mesh
            Mesh mesh = TerrainUtility.GenerateMesh(resolution, resolution, biomeMatrix);

            // Edit the mesh by applying a height modifier based on biomes
            TerrainUtility.ApplyBiomeHeightModification(ref mesh);

            // Edit the mesh by applying a height modifier based on perlin noise
            TerrainUtility.ApplyPerlinNoiseModification(ref mesh, new Vector3(parent.x, 0, parent.y), 1f, 5f);

            // Apply the mesh by setting it in the MeshFilter
            GetComponent<MeshFilter>().mesh = mesh;
        }

    }

}

// █████████████▀▀▀▀▀▀▀▀▀▀▀▀▀█████████████
// ████████▀▀░░░░░░░░░░░░░░░░░░░▀▀████████
// ██████▀░░░░░░░░░░░░░░░░░░░░░░░░░▀██████
// █████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█████
// ████░░░░░▄▄▄▄▄▄▄░░░░░░░░▄▄▄▄▄▄░░░░░████
// ████░░▄██████████░░░░░░██▀░░░▀██▄░░████
// ████░░███████████░░░░░░█▄░░▀░░▄██░░████
// █████░░▀▀███████░░░██░░░██▄▄▄█▀▀░░█████
// ██████░░░░░░▄▄▀░░░████░░░▀▄▄░░░░░██████
// █████░░░░░█▄░░░░░░▀▀▀▀░░░░░░░█▄░░░█████
// █████░░░▀▀█░█▀▄▄▄▄▄▄▄▄▄▄▄▄▄▀██▀▀░░█████
// █████░░░░░▀█▄░░█░░█░░░█░░█▄▀░░░░██▀▀▀▀▀
// ▀░░░▀██▄░░░░░░▀▀█▄▄█▄▄▄█▄▀▀░░░░▄█▀░░░▄▄
// ▄▄▄░░░▀▀██▄▄▄▄░░░░░░░░░░░░▄▄▄███░░░▄██▄
// ██████▄▄░░▀█████▀█████▀██████▀▀░░▄█████
// ███████████▄░░▀▀█▄░░░░░▄██▀▀▀░▄▄▄███▀▄█
// ████████████░██░▄██▄▄▄▄█▄░▄░████████░██﻿
// ███████████████████████████████████████
// █░░░░░░░░please play UNDERTALE░░░░░░░░█
// ███████████████████████████████████████
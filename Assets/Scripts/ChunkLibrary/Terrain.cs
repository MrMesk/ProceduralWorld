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

        // Configure chunk before loading it
        public void Set(Biome[,] _biomeMatrix, int _resolution) {
            biomeMatrix = _biomeMatrix;
            resolution = _resolution;
        }

        // Load chunk at script's start
        void Start() {
            // Generate the terrain's mesh
            Mesh mesh = TerrainUtility.GenerateMesh(resolution, resolution, biomeMatrix);

            // Edit the mesh by applying a height modifier based on biomes
            TerrainUtility.ApplyBiomeHeightModification(ref mesh);

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
// █████░░░░░▀█▄░░█░░█░░░█░░█▄▀░░░░██▀▀▀▀
// ▀░░░▀██▄░░░░░░▀▀█▄▄█▄▄▄█▄▀▀░░░░▄█▀░░░▄▄
// ▄▄▄░░░▀▀██▄▄▄▄░░░░░░░░░░░░▄▄▄███░░░▄██▄
// ██████▄▄░░▀█████▀█████▀██████▀▀░░▄█████
// ███████████▄░░▀▀█▄░░░░░▄██▀▀▀░▄▄▄███▀▄█
// ████████████░██░▄██▄▄▄▄█▄░▄░████████░██﻿
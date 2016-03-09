using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Chunk namespace containing all chunk related programing
namespace Chunk {

    public static class TerrainUtility {

        // Helper method used to generate a proper biome matrix
        public static Biome[,] GenerateBiomeMatrix (
            Biome topLeft,      Biome top,      Biome topRight,
            Biome left,         Biome center,   Biome right,
            Biome bottomLeft,   Biome bottom,   Biome bottomRight
        ) {
            // MATRIX :
            // [0,2] [1,2] [2,2]
            // [0,1] [1,1] [2,1]
            // [0,0] [1,0] [2,0]
            Biome[,] matrix = new Biome[3,3];
            matrix[0, 0] = bottomLeft;
            matrix[1, 0] = bottom;
            matrix[2, 0] = bottomRight;
            matrix[0, 1] = left;
            matrix[1, 1] = center;
            matrix[2, 1] = right;
            matrix[0, 2] = topLeft;
            matrix[1, 2] = top;
            matrix[2, 2] = topRight;

            return matrix;
        }

        public static Biome[,] GenerateBiomeMatrix( Dictionary<int, Dictionary<int, Biome>> globalBiomeMatrix, int x, int y) {
            // MATRIX :
            // [0,2] [1,2] [2,2]
            // [0,1] [1,1] [2,1]
            // [0,0] [1,0] [2,0]
            Biome[,] matrix = new Biome[3, 3];
            Dictionary<int, Biome > yAxis = null;
            Biome biomeFound;

            // Bottom Left
            if (
                globalBiomeMatrix.TryGetValue(x - 1, out yAxis) // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                matrix[0, 0] = biomeFound;
            } else {
                goto Error;
            }

            // Bottom
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                matrix[1, 0] = biomeFound;
            } else {
                goto Error;
            }

            // Bottom Right
            if (
                globalBiomeMatrix.TryGetValue(x + 1, out yAxis) // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                matrix[2, 0] = biomeFound;
            } else {
                goto Error;
            }

            // Left
            if (
                globalBiomeMatrix.TryGetValue(x - 1, out yAxis) // X
                && yAxis.TryGetValue(y, out biomeFound)         // Y
            ) {
                matrix[0, 1] = biomeFound;
            } else {
                goto Error;
            }

            // Center
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y, out biomeFound)         // Y
            ) {
                matrix[1, 1] = biomeFound;
            } else {
                goto Error;
            }

            // Right
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                matrix[2, 1] = biomeFound;
            } else {
                goto Error;
            }

            // Top Left
            if (
                globalBiomeMatrix.TryGetValue(x - 1, out yAxis) // X
                && yAxis.TryGetValue(y + 1, out biomeFound)     // Y
            ) {
                matrix[0, 2] = biomeFound;
            } else {
                goto Error;
            }

            // Top
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y + 1, out biomeFound)     // Y
            ) {
                matrix[1, 2] = biomeFound;
            } else {
                goto Error;
            }

            // Top Right
            if (
                globalBiomeMatrix.TryGetValue(x + 1, out yAxis)    // X
                && yAxis.TryGetValue(y + 1, out biomeFound)     // Y
            ) {
                matrix[2, 2] = biomeFound;
            } else {
                goto Error;
            }

            return matrix;

            // Error handler
            Error:
            return null;
        }

        // Define the pivot of the mesh at mesh generation
        private const bool _debug_centerTerrain = true;

        // Generate a terrain base mesh
        public static Mesh GenerateMesh(int width, int height, Biome[,] biomeMatrix) {

            /** BUG SHIELD **/

            // Check terrain size
            if(width < 1) {
                Debug.LogError("ERROR : Asked to generate a terrain mesh with a " + width + " polygon width.\n Needs a positive width greater than zero.");
                return null;
            }
            if (height < 1) {
                Debug.LogError("ERROR : Asked to generate a terrain mesh with a " + height + " polygon height.\n Needs a positive height greater than zero.");
                return null;
            }

            // Check biomeMatrix
            if (biomeMatrix.GetLength(0) != 3 || biomeMatrix.GetLength(1) != 3) {
                Debug.LogError("ERROR : Asked to generate a terrain mesh with a corrupted biome matrix.\n Use the GenerateBiomeMatrix() helper method to generate a proper biome matrix.");
                return null;
            }


            /** PREPARE DATA **/

            // Calculate the origin of the plane
            Vector3 origin = _debug_centerTerrain ?
                // Origin at the center
                new Vector3(-0.5f, 0, -0.5f) :
                // Origin at the bottom right corner
                Vector3.zero;
            
            // Calculate vertices generation related data
            int xAxisVertices = width + 1;
            int zAxisVertices = height + 1;
            int totalsVertices = xAxisVertices * zAxisVertices;
            Vector2 spacing = new Vector2(1f / (float)width, 1f / (float)height);
            Vector3[] vertices = new Vector3[totalsVertices];
            Vector3[] normals = new Vector3[totalsVertices];
            Vector2[] uv = new Vector2[totalsVertices];
            Color[] colors = new Color[totalsVertices];

            // Calculate triangles generation related data
            int totalTriangles = (width * height) * 2;
            int[] triangles = new int[totalTriangles * 3];


            /** GENERATE DATA **/

            // Generate vertex data
            for (int z = 0; z < zAxisVertices; ++z) {
                for (int x = 0; x < xAxisVertices; ++x) {
                    // Generate position data
                    Vector3 localVertexPosition = new Vector3(x * spacing.x, 0, z * spacing.y);
                    vertices[z * xAxisVertices + x] = origin + localVertexPosition;
                    // Generate normal data
                    normals[z * xAxisVertices + x] = Vector3.up;
                    // Generate UV data
                    uv[z * xAxisVertices + x] = new Vector2(x * spacing.x, z * spacing.y);
                    // Generate color data
                    colors[z * xAxisVertices + x] = GetVertexBiomeColor(ref biomeMatrix, localVertexPosition);
                }
            }
            
            // Generate triangles data
            for (int z = 0; z < height; ++z) {
                for (int x = 0; x < width; ++x) {
                    // POLYGON :
                    //
                    // A-B
                    // |/|
                    // C-D
                    
                    // ( Triangles must be created clockwisely )
                    
                    // |/ ABC : 1st Triangle
                    triangles[(z * width + x) * 6 + 0] = (z + 1) * xAxisVertices + (x + 0); // A
                    triangles[(z * width + x) * 6 + 1] = (z + 1) * xAxisVertices + (x + 1); // B
                    triangles[(z * width + x) * 6 + 2] = (z + 0) * xAxisVertices + (x + 0); // C

                    // /| BDC : 2nd Triangle
                    triangles[(z * width + x) * 6 + 3] = (z + 1) * xAxisVertices + (x + 1); // B
                    triangles[(z * width + x) * 6 + 4] = (z + 0) * xAxisVertices + (x + 1); // D
                    triangles[(z * width + x) * 6 + 5] = (z + 0) * xAxisVertices + (x + 0); // C
                }
            }


            /** STORE DATA IN THE MESH **/
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uv;
            mesh.colors = colors;
            mesh.triangles = triangles;


            /** RETURN MESH **/
            return mesh;
        }

        // Generate vertex color based on biomes's color code (Used for height modifier and texture lerp)
        private static Color GetVertexBiomeColor(ref Biome[,] biomeMatrix, Vector3 vertex) {
            // COLOR CODE :
            //
            // SEA          =   RED
            // BEACH        =   GREEN
            // GRASSLAND    =   BLUE
            // MOUNTAIN     =   ALPHA


            /** PREPARE DATA **/
            Vector2 position = new Vector2(vertex.x + 0.5f, vertex.z + 0.5f);
            Vector4 color = new Vector4();

            /** CALCULATE COLOR **/
            // Browse biome matrix
            for (int z = 0; z < 3; ++z) {
                for (int x = 0; x < 3; ++x) {
                    // Calculate distance
                    float distance = Vector2.Distance( new Vector2(x, z), position);
                    // Convert distance to a mask (One Minus)
                    distance = Mathf.Clamp01(-distance + 1f);
                    // Apply corresponding biome color
                    switch (biomeMatrix[x, z]) {
                        case Biome.SEA:
                            color += new Vector4(1, 0, 0, 0) * distance;
                            break;
                        case Biome.BEACH:
                            color += new Vector4(0, 1, 0, 0) * distance;
                            break;
                        case Biome.GRASSLAND:
                            color += new Vector4(0, 0, 1, 0) * distance;
                            break;
                        case Biome.MOUNTAIN:
                            color += new Vector4(0, 0, 0, 1) * distance;
                            break;
                    }
                }
            }

            /** CLAMP COLOR **/
            //float magnitude = Mathf.Clamp01(color.magnitude);
            //color = color.normalized * magnitude;
            color.Normalize();
            //color.w = Mathf.Clamp01(-color.w + 1);

            /** RETURN COLOR **/
            return color;
        }

        // Apply biome height modifier based on vertex color
        public static void ApplyBiomeHeightModification(ref Mesh mesh) {

            /** RETRIEVE DATA **/
            Vector3[] vertices = mesh.vertices;
            Color[] colors = mesh.colors;


            /** EDIT DATA **/
            for (int i = 0; i < vertices.Length; i++) {
                vertices[i] += Vector3.up * (
                    // Sea height modifier
                    (colors[i].r * -5f)
                    // Beach height modifier
                    + (colors[i].g * -2.5f)
                    // Grassland height modifier
                    + (colors[i].b * +0f)
                    // Mountain height modifier
                    + (colors[i].a * +5f)
                );
            }

            /** RETURN DATA **/
            mesh.vertices = vertices;
            mesh.colors = colors;
        }

    }
    
}
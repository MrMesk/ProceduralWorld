using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chunk {

    // Enumerated biomes
    public enum Biome : byte {
        NULL = 0,
        SEA = 1,
        BEACH = 2,
        GRASSLAND = 3,
        MOUNTAIN = 4
    }

    public static class BiomeUtility {

        public static void LoadBiome(Dictionary<int, Dictionary<int, Biome>> globalBiomeMatrix, int x, int y) {
            
            Dictionary<int, Biome> yAxis = null;
            Biome biomeFound;

            bool nextToSea = false;
            bool nextToBeach = false;
            bool nextToGrassland = false;
            bool nextToMountain = false;

            // Bottom Left
            if (
                globalBiomeMatrix.TryGetValue(x - 1, out yAxis) // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Bottom
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Bottom Right
            if (
                globalBiomeMatrix.TryGetValue(x + 1, out yAxis) // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Left
            if (
                globalBiomeMatrix.TryGetValue(x - 1, out yAxis) // X
                && yAxis.TryGetValue(y, out biomeFound)         // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Center
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y, out biomeFound)         // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Right
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y - 1, out biomeFound)     // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Top Left
            if (
                globalBiomeMatrix.TryGetValue(x - 1, out yAxis) // X
                && yAxis.TryGetValue(y + 1, out biomeFound)     // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Top
            if (
                globalBiomeMatrix.TryGetValue(x, out yAxis)     // X
                && yAxis.TryGetValue(y + 1, out biomeFound)     // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            // Top Right
            if (
                globalBiomeMatrix.TryGetValue(x + 1, out yAxis)    // X
                && yAxis.TryGetValue(y + 1, out biomeFound)     // Y
            ) {
                HandleBiomeType(biomeFound, ref nextToSea, ref nextToBeach, ref nextToGrassland, ref nextToMountain);
            }

            /**  **/

            if(nextToSea) {
                if (nextToBeach) {
                    if (nextToGrassland) {
                        if (nextToMountain) {
                            // 
                        } else {

                        }
                    } else {
                        if (nextToMountain) {

                        } else {

                        }
                    }
                } else {
                    if (nextToGrassland) {
                        if (nextToMountain) {

                        } else {

                        }
                    } else {
                        if (nextToMountain) {

                        } else {

                        }
                    }
                }
            } else {
                if (nextToBeach) {
                    if (nextToGrassland) {
                        if (nextToMountain) {

                        } else {

                        }
                    } else {
                        if (nextToMountain) {

                        } else {

                        }
                    }
                } else {
                    if (nextToGrassland) {
                        if (nextToMountain) {

                        } else {

                        }
                    } else {
                        if (nextToMountain) {

                        } else {

                        }
                    }
                }
            }

        }

        private static void HandleBiomeType(Biome biome, ref bool sea, ref bool beach, ref bool grassland, ref bool mountain) {
            switch (biome) {
                case Biome.SEA:
                    sea = true;
                    break;
                case Biome.BEACH:
                    beach = true;
                    break;
                case Biome.GRASSLAND:
                    grassland = true;
                    break;
                case Biome.MOUNTAIN:
                    mountain = true;
                    break;
            }
        }

    }

}
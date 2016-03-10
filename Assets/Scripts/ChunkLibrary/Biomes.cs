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

            /** Find biome depending on suroundings **/
            // Global biome matrix itterator setup
            Dictionary<int, Biome> yAxis = null;
            Biome biomeFound;
            // Next by biome memory setup
            bool nextToSea = false;
            bool nextToBeach = false;
            bool nextToGrassland = false;
            bool nextToMountain = false;

            /** Check suroundings **/

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

            // Right
            if (
                globalBiomeMatrix.TryGetValue(x + 1, out yAxis)     // X
                && yAxis.TryGetValue(y, out biomeFound)     // Y
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

            /** Find conforming biome **/

            if(nextToSea) {
                if (nextToBeach) {
                    if (nextToGrassland) {
                        if (nextToMountain) {
                            // Next to every biome
                            // Error
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if ( yAxis.TryGetValue(y, out biomeFound) ) {           //Y
                                yAxis[y] = Biome.NULL;
                            } else {
                                yAxis.Add(y, Biome.NULL);
                            }
                        } else {
                            // Next to sea, beach and grassland
                            // 100% Beach
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                yAxis[y] = Biome.BEACH;
                            } else {
                                yAxis.Add(y, Biome.BEACH);
                            }
                        }
                    } else {
                        if (nextToMountain) {
                            // Next to sea, beach and mountain
                            // Error
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                yAxis[y] = Biome.NULL;
                            } else {
                                yAxis.Add(y, Biome.NULL);
                            }
                        } else {
                            // Next to sea and beach
                            // 50% Sea
                            // 50% Beach
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis[y] = Biome.SEA;
                                } else {
                                    yAxis[y] = Biome.BEACH;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis.Add(y, Biome.SEA);
                                } else {
                                    yAxis.Add(y, Biome.BEACH);
                                }
                            }
                        }
                    }
                } else {
                    if (nextToGrassland) {
                        if (nextToMountain) {
                            // Next to sea, grassland and mountain
                            // Error
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                yAxis[y] = Biome.NULL;
                            } else {
                                yAxis.Add(y, Biome.NULL);
                            }
                        } else {
                            // Next to sea and grassland
                            // 100% Beach
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                yAxis[y] = Biome.BEACH;
                            } else {
                                yAxis.Add(y, Biome.BEACH);
                            }
                        }
                    } else {
                        if (nextToMountain) {
                            // Next to sea and mountain
                            // Error
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                yAxis[y] = Biome.NULL;
                            } else {
                                yAxis.Add(y, Biome.NULL);
                            }
                        } else {
                            // Next to sea
                            // 50% Sea
                            // 50% Beach
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis[y] = Biome.SEA;
                                } else {
                                    yAxis[y] = Biome.BEACH;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis.Add(y, Biome.SEA);
                                } else {
                                    yAxis.Add(y, Biome.BEACH);
                                }
                            }
                        }
                    }
                }
            } else {
                if (nextToBeach) {
                    if (nextToGrassland) {
                        if (nextToMountain) {
                            // Next to beach grassland and mountain
                            // 100% Grassland
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                yAxis[y] = Biome.GRASSLAND;
                            } else {
                                yAxis.Add(y, Biome.GRASSLAND);
                            }
                        } else {
                            // Next to beach and grassland
                            // 50% Beach
                            // 50% Grassland
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis[y] = Biome.BEACH;
                                } else {
                                    yAxis[y] = Biome.GRASSLAND;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis.Add(y, Biome.BEACH);
                                } else {
                                    yAxis.Add(y, Biome.GRASSLAND);
                                }
                            }
                        }
                    } else {
                        if (nextToMountain) {
                            // Next to beach and mountain
                            // 100% Grassland
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                yAxis[y] = Biome.GRASSLAND;
                            } else {
                                yAxis.Add(y, Biome.GRASSLAND);
                            }
                        } else {
                            // Next to beach
                            // 33% Sea
                            // 34% Beach
                            // 33% Grassland
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.67f) {
                                    yAxis[y] = Biome.SEA;
                                } else if (r > 0.34f) {
                                    yAxis[y] = Biome.BEACH;
                                } else {
                                    yAxis[y] = Biome.GRASSLAND;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.67f) {
                                    yAxis.Add(y, Biome.SEA);
                                } else if (r > 0.34f) {
                                    yAxis.Add(y, Biome.BEACH);
                                } else {
                                    yAxis.Add(y, Biome.GRASSLAND);
                                }
                            }
                        }
                    }
                } else {
                    if (nextToGrassland) {
                        if (nextToMountain) {
                            // Next to grassland and mountain
                            // 50% Grassland
                            // 50% Mountain
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis[y] = Biome.GRASSLAND;
                                } else {
                                    yAxis[y] = Biome.MOUNTAIN;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.5f) {
                                    yAxis.Add(y, Biome.GRASSLAND);
                                } else {
                                    yAxis.Add(y, Biome.MOUNTAIN);
                                }
                            }
                        } else {
                            // Next to grassland
                            // 33% Beach
                            // 34% Grassland
                            // 33% Mountain
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.67f) {
                                    yAxis[y] = Biome.BEACH;
                                } else if (r > 0.34f) {
                                    yAxis[y] = Biome.GRASSLAND;
                                } else {
                                    yAxis[y] = Biome.MOUNTAIN;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.67f) {
                                    yAxis.Add(y, Biome.BEACH);
                                } else if (r > 0.34f) {
                                    yAxis.Add(y, Biome.GRASSLAND);
                                } else {
                                    yAxis.Add(y, Biome.MOUNTAIN);
                                }
                            }
                        }
                    } else {
                        if (nextToMountain) {
                            // Next to mountain
                            // 50% Grassland
                            // 50% Mountain
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.5) {
                                    yAxis[y] = Biome.GRASSLAND;
                                } else {
                                    yAxis[y] = Biome.MOUNTAIN;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.5) {
                                    yAxis.Add(y, Biome.GRASSLAND);
                                } else {
                                    yAxis.Add(y, Biome.MOUNTAIN);
                                }
                            }
                        } else {
                            // Nothing around
                            // 25% Sea
                            // 25% Beach
                            // 25% Grassland
                            // 25% Mountain
                            if (!globalBiomeMatrix.TryGetValue(x, out yAxis)) {     //X
                                yAxis = new Dictionary<int, Biome>();
                                globalBiomeMatrix.Add(x, yAxis);
                            }
                            if (yAxis.TryGetValue(y, out biomeFound)) {             //Y
                                float r = Random.value;
                                if (r > 0.75f) {
                                    yAxis[y] = Biome.SEA;
                                } else if (r > 0.50f) {
                                    yAxis[y] = Biome.BEACH;
                                } else if (r > 0.25f) {
                                    yAxis[y] = Biome.GRASSLAND;
                                } else {
                                    yAxis[y] = Biome.MOUNTAIN;
                                }
                            } else {
                                float r = Random.value;
                                if (r > 0.75f) {
                                    yAxis.Add(y, Biome.SEA);
                                } else if (r > 0.50f) {
                                    yAxis.Add(y, Biome.BEACH);
                                } else if (r > 0.25f) {
                                    yAxis.Add(y, Biome.GRASSLAND);
                                } else {
                                    yAxis.Add(y, Biome.MOUNTAIN);
                                }
                            }
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
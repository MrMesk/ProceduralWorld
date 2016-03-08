using UnityEngine;
using System.Collections;

public class WorldGeneration : MonoBehaviour
{
	public enum BiomeType
	{
		plain,
		beach,
		ocean,
		mountain
	}

	public BiomeType biome;
	public BiomeType biomeToSpawn;

	BiomeType leftBiome;
	BiomeType rightBiome;
	BiomeType topBiome;
	BiomeType botBiome;

	[Header("Probabilities")]

	[Space(10)]
	[Range(0.0f, 100.0f)]
	public float probaBeachPlain;
	[Range(0.0f, 100.0f)]
	public float probaBeachBeach;
	[Range(0.0f, 100.0f)]
	public float probaPlainMountain;
	[Range(0.0f, 100.0f)]
	public float probaPlainBeach;
	[Range(0.0f, 100.0f)]
	public float probaPlainPlain;
	[Range(0.0f, 100.0f)]
	public float probaOceanBeach;
	[Range(0.0f, 100.0f)]
	public float probaBeachOcean;

	bool oceanNearby;

	// Use this for initialization
	void Start ()
	{
		
            
	}

	void SpawnBiome (Vector3 pos)
	{
		float rand = 0;
		switch (biome)
		{
			case BiomeType.beach:
			rand = Random.Range(0, probaBeachPlain + probaBeachBeach);
			if (rand >= probaBeachPlain)
			{
				biomeToSpawn = BiomeType.beach;
			}
			else
			{
				biomeToSpawn = BiomeType.plain;
			}
			break;

			case BiomeType.mountain:

			break;

			case BiomeType.ocean:

			break;

			case BiomeType.plain:

			break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class SpawnVeggies : MonoBehaviour
{
	GrowRandomVegetation spawner;
	// Use this for initialization
	void Start () {
		spawner = GetComponent<GrowRandomVegetation>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			Vector3 pos = new Vector3(Random.Range(-20f,20f),0f, Random.Range(-20f, 20f));
			spawner.growVegetation(pos.x, pos.y, pos.z);
		}
	}
}

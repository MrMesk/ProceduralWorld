using UnityEngine;
using System.Collections;

public class GrowRandomVegetation : MonoBehaviour
{
	[Range (0f,90f)]
	public float baseBranchSpread = 25f; // Base branch spread, will be affected by a random factor so all trees don't look the same
	[Range(0f,100f)]
	public float randomness = 100.0f; // Random factor so the trees look a lot more natural

	public Material[] branchMats = new Material[3];
	public GameObject[] endVegetation = new GameObject[3];
	public GameObject endMesh;

	private Vector3 endScale;
	private int veggieIndex;
	private float scale = 0.5f;
	private LineRenderer branchLine;
	private GameObject fractalVegetation;
	private GameObject branch;
	private int vegetationSize; // Number of iterations

	void Start()
	{
		Vector3 pos = transform.position;
		growVegetation(pos.x, pos.y, pos.z);
	}

	public void growVegetation (float x, float y, float z)
	{
		vegetationSize = Random.Range(2, 6); // Random between 4 and 6 to randomly create bushes, little and tall trees
		
		if (vegetationSize < 3)
		{
			veggieIndex = 0;
			endScale = new Vector3(0.5f, 0.5f, 0.5f);
			scale = Random.Range(0.8f, 1f);
		}
		else if (vegetationSize == 3)
		{
			veggieIndex = 1;
			endScale = new Vector3(0.7f, 0.7f, 0.7f);
			scale = Random.Range(1.2f, 1.5f);
		}
		else
		{
			veggieIndex = 2;
			endScale = new Vector3(1.5f, 1.5f, 1.5f);
			scale = Random.Range(1.5f, 2f);
		}

		// Creating an empty gameObject to store our fractal
		fractalVegetation = new GameObject("Fractal_Vegetation");
		// We set the vegetation as a child
		fractalVegetation.transform.parent = gameObject.transform;
		// Call generation method
		drawTree(x, y, z, y, 90.0f, 90.0f, vegetationSize);
	}

	//Recursively draws the fractal vegetation
	void drawTree (float x1, float y1, float z1, float y3, float angle, float angle2, int vegetationSize)
	{
		if (vegetationSize > 0)
		{
			//Set the x end point
			float x2 = x1 + (Mathf.Cos(Mathf.Deg2Rad * angle) * vegetationSize * scale);

			//Set the z end point
			float z2 = z1 + (Mathf.Cos(Mathf.Deg2Rad * angle2) * vegetationSize * scale);

			//Set the y end point
			float y2 = y1 + (Mathf.Sin(Mathf.Deg2Rad * angle) * vegetationSize * scale);

			//Set the y4 point
			float y4 = y3 + (Mathf.Sin(Mathf.Deg2Rad * angle2) * vegetationSize * scale);

			//Average the y values
			float n1 = (y3 + y1) / 2;
			float n2 = (y4 + y2) / 2;

			//Calling the branch drawing function
			drawBranch(x1, n1, z1, x2, n2, z2, vegetationSize);

			//Iterate the function recursively, and change each branch rotation
			drawTree(x2, y2, z2, y4, angle - (baseBranchSpread - Random.value * randomness), angle2 - (baseBranchSpread - Random.value * randomness), vegetationSize - 1);
			drawTree(x2, y2, z2, y4, angle + (baseBranchSpread - Random.value * randomness), angle2 + (baseBranchSpread - Random.value * randomness), vegetationSize - 1);
			drawTree(x2, y2, z2, y4, angle + (baseBranchSpread - Random.value * randomness), angle2 - (baseBranchSpread - Random.value * randomness), vegetationSize - 1);
			drawTree(x2, y2, z2, y4, angle - (baseBranchSpread - Random.value * randomness), angle2 + (baseBranchSpread - Random.value * randomness), vegetationSize - 1);

		}
	}
	void drawLeaf(float x, float y, float z, Vector3 dir)
	{
		GameObject leaf = Instantiate(endVegetation[veggieIndex]);
		leaf.name = "Leaf";
		leaf.transform.parent = fractalVegetation.transform;

		leaf.transform.position = new Vector3(x, y, z);
		leaf.transform.localScale = endScale;
		leaf.transform.up = dir;
	}
	//This function draws a single branch from x1,y1 and z1 coordinates, to x2,y2 and z2 and set its width accordingly to the actual state of the generation
	void drawBranch (float x1, float y1, float z1, float x2, float y2, float z2, int generationIndex)
	{
		//Create an empty GameObject to store the branch
		branch = new GameObject("Branch");

		//Set the branch as child of the main GameObject
		branch.transform.parent = fractalVegetation.transform;

		//Add it a line renderer
		branchLine = branch.AddComponent<LineRenderer>() as LineRenderer;

		//Affect it a material we set in the hierarchy
		branchLine.material = branchMats[veggieIndex];

		//Thin the branches through each iteration
		branchLine.SetWidth(generationIndex * 0.06f * 2 * scale, generationIndex * 0.04f * 2 * scale);

		//Draw the line.
		branchLine.SetPosition(0, new Vector3(x1, y1, z1));
		branchLine.SetPosition(1, new Vector3(x2, y2, z2));

		if (generationIndex == 1)
		{
			Vector3 direction = new Vector3(x2, y2, z2) - new Vector3(x1, y1, z1);
			drawLeaf(x2, y2, z2, direction);
		}
			
	}
}
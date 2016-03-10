using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour
{
	public float speed;
	public float rotationSpeed;

	
	// Update is called once per frame
	void Update ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if(h != 0)
		{
			transform.Rotate(Vector3.up, rotationSpeed * h * Time.deltaTime);
		}

		if(v !=0)
		{
			transform.Translate(transform.forward * speed * Time.deltaTime * v, Space.World);
		}
	}
}

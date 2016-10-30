using UnityEngine;
using System.Collections;

public class MovementController : MonoBehaviour
{
	public float speed = 5;

	void Update()
	{
		float v = Input.GetAxis("Vertical");

		transform.Translate(Vector2.up * v * speed * Time.deltaTime);
	}
}

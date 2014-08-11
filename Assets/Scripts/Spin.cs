using UnityEngine;
using System.Collections;

public class Spin : MonoBehaviour
{
	public Vector3 Axis = new Vector3(0.0f, 1.0f, 0.0f);
	public float AnglePerSecond;

	private Transform tr;


	void Awake()
	{
		tr = transform;
	}
	void FixedUpdate()
	{
		tr.Rotate(Axis.normalized, AnglePerSecond * Time.deltaTime);
	}
}
using UnityEngine;
using System.Collections;

public class WaterLayer : MonoBehaviour {
	public uint depth;
	public float circleRadius = 0.3f;
	public float revolutionSeconds = 1.0f;
	public Vector3 initialPosition;

	private float rotationSpeed = 0.0f;
	private float angle = 0.0f;

	// Use this for initialization
	void Start () {
		rotationSpeed = (2.0f * Mathf.PI) / revolutionSeconds;
	}
	
	// Update is called once per frame
	void Update () {
		angle += rotationSpeed * Time.deltaTime;
		transform.position = 
			initialPosition + 
			new Vector3(
				Mathf.Cos (angle) * circleRadius,
				Mathf.Sin (angle) * circleRadius,
				0.0f
			);
	}
}

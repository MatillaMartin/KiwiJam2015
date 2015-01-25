using UnityEngine;
using System.Collections;

public class WaterLayer : MonoBehaviour {
	public int depth;
	public float circleRadius = 0.3f;
	public float revolutionSeconds = 1.0f;

	private Vector3 initialPosition;
	private float rotationSpeed = 0.0f;
	private float angle = 0.0f;

	void Start(){
		float viewPortWidth = 
			Camera.main.ViewportToWorldPoint(new Vector3( 1.0f, 1.0f, Camera.main.nearClipPlane)).x - 
				Camera.main.ViewportToWorldPoint(new Vector3( 0.0f, 0.0f, Camera.main.nearClipPlane)).x;
		float scaleUp = viewPortWidth / renderer.bounds.size.x * (1.0f + circleRadius);
		Vector3 newScale = scaleUp * transform.localScale;
		transform.localScale = newScale;
	}
	

	// Use this for initialization
	void Awake () {
		initialPosition = transform.localPosition; 
		rotationSpeed = (2.0f * Mathf.PI) / revolutionSeconds;
		
	}


	// Update is called once per frame
	void Update () {
		angle += rotationSpeed * Time.deltaTime;
		transform.localPosition = 
			initialPosition + 
			new Vector3(
				Mathf.Cos (angle) * circleRadius,
				Mathf.Sin (angle) * circleRadius,
				0.0f
			);
	}
}

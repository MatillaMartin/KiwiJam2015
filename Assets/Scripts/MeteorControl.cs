using UnityEngine;
using System.Collections;

public class MeteorControl : MonoBehaviour {

	private GameObject spawnerLeft;
	private GameObject spawnerRight;
	
	
	// Use this for initialization
	void Start () {
		spawnerLeft = transform.Find ("Meteorite Spawn Left").gameObject;
		spawnerRight = transform.Find ("Meteorite Spawn Right").gameObject;
		
		float offset = 0.1f;
		
		Vector3 spawnerPos = 
			Camera.main.ViewportToWorldPoint(new Vector3( 0.5f, 1.0f + offset, 0.0f));
		transform.position = spawnerPos;
		
		float viewPortWidth = 
			Camera.main.ViewportToWorldPoint(new Vector3( 1.0f, 1.0f, Camera.main.nearClipPlane)).x - 
				Camera.main.ViewportToWorldPoint(new Vector3( 0.0f, 0.0f, Camera.main.nearClipPlane)).x;
		float scaleUp = viewPortWidth / collider2D.bounds.size.x * (1.0f - offset);
		Vector3 newScale = scaleUp * transform.localScale;
		transform.localScale = newScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

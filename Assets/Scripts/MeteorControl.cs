using UnityEngine;
using System.Collections;

public class MeteorControl : MonoBehaviour {

	private MeteoriteSpawn spawnerLeft;
	private MeteoriteSpawn spawnerRight;
	
	
	// Use this for initialization
	void Start () {
		spawnerLeft = transform.Find ("Meteorite Spawn Left").gameObject.GetComponent<MeteoriteSpawn>();
		spawnerRight = transform.Find ("Meteorite Spawn Right").gameObject.GetComponent<MeteoriteSpawn>();
		
		float offset = 0.1f;
		
		Vector3 spawnerPos = 
			Camera.main.ViewportToWorldPoint(new Vector3( 0.5f, 1.0f + offset, transform.position.z));
		transform.position = spawnerPos;
		
		float viewPortWidth = 
			Camera.main.ViewportToWorldPoint(new Vector3( 1.0f, 1.0f, Camera.main.nearClipPlane)).x - 
			Camera.main.ViewportToWorldPoint(new Vector3( 0.0f, 0.0f, Camera.main.nearClipPlane)).x;
		float currentWidth =
			spawnerRight.collider2D.bounds.max.x - 
			spawnerLeft.collider2D.bounds.min.x;
		float scaleUp = viewPortWidth / currentWidth;
		Vector3 newScale = scaleUp * transform.localScale;
		transform.localScale = newScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	public void SpawnLeft()
	{
		spawnerLeft.spawnMeteor();
	}
	
	public void SpawnRight()
	{
		spawnerRight.spawnMeteor();
	}
}

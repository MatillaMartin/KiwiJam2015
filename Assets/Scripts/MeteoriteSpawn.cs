using UnityEngine;
using System.Collections;

public class MeteoriteSpawn : MonoBehaviour {
	public GameObject Meteorite;
	public Vector2 spawnTimerRange = new Vector2(10.0f, 20.0f);
	private float spawnTimer = 5.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(spawnTimer < 0.0f)
		{
			spawnMeteor();	
			spawnTimer = Random.Range(spawnTimerRange.x, spawnTimerRange.y);
		} else 
		{
			spawnTimer -= Time.deltaTime;
		}
	}
	
	public void spawnMeteor()
	{
		Instantiate(
			Meteorite, 
			new Vector3(Random.Range(
				this.collider2D.bounds.min.x, this.collider2D.bounds.max.x), 
				this.collider2D.bounds.center.y, 
				transform.position.z), 
			transform.rotation);
	}
}

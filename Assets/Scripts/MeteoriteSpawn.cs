using UnityEngine;
using System.Collections;

public class MeteoriteSpawn : MonoBehaviour {
	public GameObject Meteorite;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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

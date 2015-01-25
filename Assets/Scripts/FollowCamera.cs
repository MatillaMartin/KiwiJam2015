using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	public bool follow = true;
	public GameObject background;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(follow)
			transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + background.renderer.bounds.size.y / 4.0f, transform.position.z);
	}
}

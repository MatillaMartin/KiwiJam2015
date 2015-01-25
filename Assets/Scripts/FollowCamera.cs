using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	public bool follow = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(follow)
			transform.position = Camera.main.transform.position;
	}
}

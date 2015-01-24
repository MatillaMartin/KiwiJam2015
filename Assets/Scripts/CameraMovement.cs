using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {
	public float cameraSpeed = 0.1f;
	public List<GameObject> parallaxObject;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0,Time.deltaTime * cameraSpeed,0));

	}

}

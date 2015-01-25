using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {
	public float cameraSpeed = 0.1f;
	public List<GameObject> parallaxObject;
	public string wallScript;

	// Use this for initialization
	void Start () {
		parseWallScript ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (0,Time.deltaTime * cameraSpeed,0));

	}

	void parseWallScript(){
		// Input tile order 

		// wall4 tileable
		// wall3 tileable
		// wall2
		// wall1 tileable
		// wall1 tileable
		// base
	}
	
	public void setCameraSpeedVertical(float v)
	{
		cameraSpeed = v;
	}
}

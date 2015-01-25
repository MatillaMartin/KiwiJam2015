using UnityEngine;
using System.Collections;

public class JumpingBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		//if is in JumpingPlatform layer
		Debug.Log ("Trigering jumping!!!");

	}
}

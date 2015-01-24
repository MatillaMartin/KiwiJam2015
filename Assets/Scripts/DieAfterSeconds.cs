using UnityEngine;
using System.Collections;

public class DieAfterSeconds : MonoBehaviour {
	public float seconds = 5.0f;
	
	// Use this for initialization
	void Start () {
	}
	
	void Awake()
	{
		Destroy(gameObject, seconds);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
